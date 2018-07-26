using Common;
using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;
using Services.Mapping;
using System.Configuration;
using Repostories;
using System.Transactions;

namespace Services
{
    public class CaptureService : ICaptureService
    {
        [Dependency]
        public ILayoutRepository _layoutRepository { get; set; }

        [Dependency]
        public ICaptureRepository _repository { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }

        [Dependency]
        public IAlarmRepository _alarmRepository { get; set; }


        [Dependency]
        public IMessageRepository _messageRepository { get; set; }

        #region 抬杠密钥
        private char[] chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public string GetRandomNumberString(int int_NumberLength)
        {
            Random random = new Random();
            string validateCode = string.Empty;
            for (int i = 0; i < int_NumberLength; i++)
                validateCode += chars[random.Next(0, chars.Length)].ToString();
            return validateCode;
        }

        #endregion

        public CaptureDTO GetById(int id)
        {
            var capture = _repository.GetById(id);
            if (capture.IsNotNull())
                return capture.ConvertoDto<Capture, CaptureDTO>();

            return null;
        }

        public Resp_Binary_Member<AlarmDTO> Add_One(CaptureDTO model)
        {
            var alert = false;
            var capture = model.GetPrototype<CaptureDTO, Capture>();

            if (capture.Pass == 1)  //without代表该车出场
                capture.WithOut = 0;
            if (capture.Pass == 0)
                capture.WithOut = 1;


            var result = false;
            var guid = Guid.NewGuid().ToString();

            long layoutId = 0;

            using (var context = new SKContext())
            {
                using (var tran = new TransactionScope())
                {
                    var layout = context.Layouts.Where(d => d.CarNumber == model.CarNumber && d.IsValid == 1 && d.Channel == model.Channel).FirstOrDefault();
                    if (layout.IsNotNull())
                    {
                        if (layout.TriggerType == 2 || layout.TriggerType == model.Pass.ToInt()) //进出场控制
                        {
                            alert = true;
                            var lettercode = GetRandomNumberString(ConfigPara.LetterCount);

                            var alarm = new Alarm
                            {
                                CarNumber = model.CarNumber,
                                IsDeal = 0,
                                GUID = guid,
                                AlarmTime = DateTime.Now,
                                Channel=model.Channel,
                                LetterCode = AESEncryptHelper.Encrypt(lettercode)
                            };

                            _messageRepository.SendMessage(capture.ToJson(), capture.Channel);
                            capture.Alarms.Add(alarm);

                            layoutId = layout.ID;
                            //if (layout.Degree > 0)
                            //    --layout.Degree;
                            //if (layout.Degree == 0)
                            //    layout.IsValid = 0;
                        }

                    }

                    context.Captures.Add(capture);
                    var captureback = model.GetPrototype<CaptureDTO, CaptureBackup>();
                    context.CaptureBackups.Add(captureback); 

                    //当车出场时，将该车的进场记录的是否出场改为已出场
                    if (model.Pass.ToInt() == 0)
                    {
                        var in_captures = context.Captures.Where(t => t.CarNumber == model.CarNumber && t.Pass == 1 && t.WithOut == 0);
                        foreach (var item in in_captures)
                        {
                            item.WithOut = 1;
                        }
                    }

                    var recordM = context.RecordManagers.Where(t => t.CarNumber == model.CarNumber).OrderByDescending(t => t.ID).FirstOrDefault();
                    if (recordM.IsNotNull())
                    {
                        if (model.Pass.ToInt() == 1) //进场修改备案最后一次进场时间
                            recordM.LastInTime = model.CreateTime.ToDateTime();
                        else if (model.Pass.ToInt() == 0) //出场修改备案最近一次出场时间
                            recordM.LastOutTime = model.CreateTime.ToDateTime();
                    }

                    context.SaveChanges();
                    tran.Complete();
                    result = true;
                }
            }



            if (result)
            {
                if (alert)
                {
                    //    var req_dock = new Req_Warning
                    //    {
                    //        WARNINGDATE = DateTime.Now.ToString("yyyy-MM-dd hh: mm:ss"),
                    //        CARNO = model.CarNumber,
                    //        CARTYPE = model.CarType,
                    //        inout = model.Pass.Equals("1") ? "进" : "出",
                    //        bayonet = model.Channel,
                    //        Remark = model.Remark

                    //    };
                    //    Docking(req_dock); //报警通知第三方

                    var alam = _alarmRepository.GetByWhere(t => t.GUID == guid).FirstOrDefault();
                    var dto = alam.ConvertoDto<Alarm, AlarmDTO>();
                    dto.LetterCode = alam.LetterCode;
                    return new Resp_Binary_Member<AlarmDTO> { message = "该车辆已中控报警！", flag = 2, LetterCode = dto.LetterCode, entity = dto, LayoutId = layoutId };
                }
                return new Resp_Binary_Member<AlarmDTO> { message = "添加成功", flag = 1 };

            }

            return new Resp_Binary_Member<AlarmDTO> { message = "添加失败", flag = 0 };
        }






        /// <summary>
        /// 对接第三方系统
        /// </summary>
        /// <param name="warn"></param>
        public void Docking(Req_Warning warn)
        {
            var dock_url = ConfigPara.DockUrl;
            var dock_tableName = ConfigPara.DockTableName;
            var list = new List<Req_Warning>();
            list.Add(warn);
            var warnings = Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(list.ToJson()))));
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("tableName", dock_tableName);
                parameters.Add("warnings", warnings);
                var i = 3;
                while (i > 0)  //发送失败则发送三次
                {
                    var response = WebRequestCommon.CreatePostHttpResponse(dock_url, parameters);
                    var obj = WebRequestCommon.GetResponseString(response).ToObject<Resp_Warning>();
                    if (obj.Success)
                    {
                        break;
                    }
                    else
                    {
                        CreateLogError(dock_url, warnings, obj.msg);
                    }
                    i--;
                }
            }
            catch (Exception ex)
            {
                CreateLogError(dock_url, warnings, ex.Message);
            }
        }

        /// <summary>
        /// 记录对接日志
        /// </summary>
        /// <param name="dock_url"></param>
        /// <param name="warnings"></param>
        /// <param name="message"></param>
        private void CreateLogError(string dock_url, string warnings, string message)
        {
            var log = new SysErrorLog
            {
                ErrReferrer = dock_url,
                ErrSource = warnings,
                ErrTime = DateTime.Now,
                ErrTimestr = DateTime.Now.ToString("yyyyMMdd"),
                ErrStack = "第三方对接",
                ErrType = SysErrorType.docking.ToString(),
                ErrUrl = dock_url,
                ErrIp = "",
                ErrMessage = message
            };

            using (SKContext context = new SKContext())
            {
                context.SysErrorLogs.Add(log);
                context.SaveChanges();
            }
        }


        public Resp_Query<CaptureDTO> Query(Capture_Query request)
        {
            var records = _repository.GetAll();
            var response = new Resp_Query<CaptureDTO>();
            records.ToMaybe()
                .Do(d => request.Verify())
                .DoWhen(t => !string.IsNullOrEmpty(request.CarNumber), d => records = records.Where(s => s.CarNumber.Contains(request.CarNumber.Trim())))
                .DoWhen(t => !string.IsNullOrEmpty(request.Channel), d => records = records.Where(s => s.Channel.Equals(request.Channel)))
                .DoWhen(t => !string.IsNullOrEmpty(request.ParkId), d => records = records.Where(s => s.ParkId.Contains(request.ParkId.Trim())));

            if (!string.IsNullOrEmpty(request.Pass) && int.TryParse(request.Pass, out int result))
            {
                if (result == 2)
                    records = records.Where(s => s.Pass == 1 && s.WithOut == 0);
                else
                    records = records.Where(s => s.Pass == result);
            }

            if (!string.IsNullOrEmpty(request.StayHours) && int.TryParse(request.StayHours, out int stayHours))
            {
                var dt = DateTime.Now.AddHours(-stayHours);
                records = records.Where(t => t.CreateTime < dt);
            }

            if (!string.IsNullOrEmpty(request.BeginTime) && DateTime.TryParse(request.BeginTime, out DateTime start))  //Linq to entity 不支持datatime.parse函数
                records = records.Where(s => s.CreateTime >= start);

            if (!string.IsNullOrEmpty(request.EndTime) && DateTime.TryParse(request.EndTime, out DateTime end))
                records = records.Where(s => s.CreateTime <= end);

            response.totalCounts = records.Count();
            response.totalRows = (int)Math.Ceiling((double)response.totalCounts / request.PgSize);

            if (!string.IsNullOrEmpty(request.Order))
                records = records.DataSorting(request.Order, request.Esc);
            else
                records = records.OrderByDescending(o => o.ID);

            response.entities = records.Skip(request.PgSize * (request.PgIndex - 1)).Take(request.PgSize).ToList().ConvertoDto<Capture, CaptureDTO>().ToList();
            return response;
        }

        public Resp_Index<CaptureDTO> Index(Req_Index request)
        {
            var response = new Resp_Index<CaptureDTO>();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (limits.IsNotNull() && limits.Count > 0 && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits.OrderByDescending(t => t.IsValid).GroupBy(t => new { t.KeyCode, t.KeyName }).Select(s => new SysModuleOperateIndexDTO
                {
                    KeyCode = s.Key.KeyCode,
                    KeyName = s.Key.KeyName,
                    IsValid = s.Sum(x => x.IsValid),
                }).ToList();
                var query_parameter = new Capture_Query { PgIndex = 1, PgSize = request.PgSize };
                response.query = Query(query_parameter);
            }
            else
            {
                _sysRightRepository.UnitOfWork.Commite();
                response.allowVisit = false;
                response.message = "无访问权限，请联系管理员";
            }

            return response;

        }

        public Resp_Binary Decrease(long id)
        {
            var layout = _layoutRepository.GetById<long>(id);
            if (layout.IsNotNull())
            {
                if (layout.Degree > 0)
                    --layout.Degree;
                if (layout.Degree == 0)
                    layout.IsValid = 0;

                if (_layoutRepository.UnitOfWork.Commite() > 0)
                    return Resp_Binary.Modify_Sucess;
                else
                    return Resp_Binary.Modify_Failed;
            }
            else
            {
                return new Resp_Binary { message = "未找到对应布控对象" };
            }


        }
    }
}
