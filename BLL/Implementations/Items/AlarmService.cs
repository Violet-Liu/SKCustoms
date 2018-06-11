using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;
using Services.Mapping;
using Common;
using Infrastructure;

namespace Services
{
    public partial class AlarmService : IAlarmService
    {
        [Dependency]
        public IAlarmRepository _repository { get; set; }

        [Dependency]
        public ICaptureRepository _capture_repository { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }



        public Resp_Binary CreateOrUpdate(AlarmDTO model)
        {
            var exsits = _repository.GetByWhere(t => t.GUID == model.GUID).FirstOrDefault();
            if (exsits.IsNull() || string.IsNullOrEmpty(model.GUID))
            {
                var alarm = model.GetPrototype<AlarmDTO, Alarm>();
                if (alarm.CaptureId > 0)
                {
                    var capture = _capture_repository.GetById(alarm.CaptureId);
                    if (capture == null)
                        alarm.CaptureId = null;
                }

                if (string.IsNullOrEmpty(model.HandlerTime))
                    alarm.HandlerTime = null;

                if (string.IsNullOrEmpty(model.GUID))
                    model.GUID = Guid.NewGuid().ToString();

                _repository.Insert(alarm);
            }
            else
            {
                exsits.IsDeal = model.IsDeal;
                exsits.Operator = model.Operator;
                exsits.Message = model.Message;
                if (!string.IsNullOrEmpty(model.HandlerTime))
                    exsits.HandlerTime = model.HandlerTime.ToDateTime();
                else
                    exsits.HandlerTime = null;

                _repository.Update(exsits);
            }

            if (_repository.UnitOfWork.Commite() > 0)
            {
                return Resp_Binary.Add_Sucess;
            }

            

            return Resp_Binary.Add_Failed;
        }


        public Resp_Binary Check(Alarm_Check request)
        {
            var model = _repository.GetById(request.ID);
            if(model.IsNotNull())
            {
                model.IsDeal = request.IsDeal;
                model.Operator = request.Operator;
                model.Message = request.Message;
                model.HandlerTime = request.HandlerTime.ToDateTime();

                _repository.Update(model);
                if(_repository.UnitOfWork.Commite()>0)
                {
                    return Resp_Binary.Add_Sucess;
                }
            }
            return Resp_Binary.Add_Failed;

        }

        public Resp_Query<AlarmDTO> Query(Alarm_Query request)
        {
            var records = _repository.GetAll();
            var response = new Resp_Query<AlarmDTO>();
            records.ToMaybe()
                .Do(d => request.Verify())
                .DoWhen(t => !string.IsNullOrEmpty(request.CarNumber), d => records = records.Where(s => s.CarNumber.Contains(request.CarNumber)));


            if(!string.IsNullOrEmpty(request.IsDeal)&&int.TryParse(request.IsDeal,out int result))
            {
                records = records.Where(s => s.IsDeal == result);
            }

            if (!string.IsNullOrEmpty(request.AlarmBeginTime) && DateTime.TryParse(request.AlarmBeginTime, out DateTime a_start))  
            {
                records = records.Where(s => s.AlarmTime >= a_start);
            }

            if (!string.IsNullOrEmpty(request.AlarmEndTime) && DateTime.TryParse(request.AlarmEndTime, out DateTime a_end))
            {
                records = records.Where(s => s.AlarmTime <= a_end);
            }

            if (!string.IsNullOrEmpty(request.HandlerBeginTime) && DateTime.TryParse(request.HandlerBeginTime, out DateTime h_start))
            {
                records = records.Where(s => s.HandlerTime >= h_start);
            }
            if (!string.IsNullOrEmpty(request.HandlerEndTime) && DateTime.TryParse(request.HandlerEndTime, out DateTime h_end))
            {
                records = records.Where(s => s.HandlerTime <= h_end);
            }

            response.totalCounts = records.Count();
            response.totalRows = (int)Math.Ceiling((double)response.totalCounts / request.PgSize);
            

            if (!string.IsNullOrEmpty(request.Order))
            {
                records = records.DataSorting(request.Order, request.Esc);
            }
            else
            {
                records = records.OrderByDescending(o => o.ID);
            }

            response.entities= records.Skip(request.PgSize * (request.PgIndex - 1)).Take(request.PgSize).ToList().ConvertoDto<Alarm, AlarmDTO>().ToList();

            return response;
        }


        public Resp_Index<AlarmDTO> Index(Req_Index request)
        {
            var response = new Resp_Index<AlarmDTO>();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (!limits.IsNullOrEmpty() && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits;
                var query_parameter = new Alarm_Query { PgIndex = 1, PgSize = request.PgSize };
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

        public Resp_Binary Add(AlarmDTO model)
        {
            var alarm = model.GetPrototype<AlarmDTO, Alarm>();
            if (alarm.CaptureId > 0)
            {
                var capture = _capture_repository.GetById(alarm.CaptureId);
                if (capture == null)
                    alarm.CaptureId = null;
            }

            if (string.IsNullOrEmpty(model.HandlerTime))
                alarm.HandlerTime = null;

            if (string.IsNullOrEmpty(model.GUID))
                model.GUID = Guid.NewGuid().ToString();

            _repository.Insert(alarm);
            if (_repository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Add_Sucess;

            return Resp_Binary.Add_Failed;
        }

        //public Resp_Binary CheckPermission(Req_Index index)
        //{
        //    var limit = _sysRightRepository.GetRightByUserWithModule(index.userId, "报警检查", "修改");

        //}
    }
}
