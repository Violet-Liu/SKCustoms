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
using System.Collections;
using System.IO;
using LinqToExcel;


namespace Services
{
    public partial class LayoutService : ILayoutService
    {
        [Dependency]
        public ILayoutRepository _repository { get; set; }

        [Dependency]
        public ISysErrorLogRepository _error { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }


        public Resp_Binary Add_Batch(IEnumerable<LayoutDTO> models)
        {
            if (models.Count() < 5000)
                BulkOperation<Layout>.MySqlBulkInsert(models.GetPrototype<LayoutDTO, Layout>(), _repository);
            else
                BulkOperation<Layout>.MySqlBulkInsertAsync(models.GetPrototype<LayoutDTO, Layout>(), _repository);

            return Resp_Binary.Add_Sucess;
        }

        public Resp_Binary Add_One(LayoutDTO model)
        {
            var old_model = _repository.GetByWhere(t => t.CarNumber == model.CarNumber).FirstOrDefault();
            if (old_model.IsNotNull())
            {
                var response = new Resp_Binary
                {
                    message = "该车牌号已在备案库，是否进行替换"
                };
                return response;
            }
            _repository.Insert(model.GetPrototype<LayoutDTO,Layout>());
            if (_repository.UnitOfWork.Commite() > 0)
            {
                return Resp_Binary.Add_Sucess;
            }
            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Del(Base_SingleDel model)
        {
            if (model.Id == 0)
                return new Resp_Binary { message = "请选择要操作的记录" };

            _repository.DeleteById(model.Id);
            if (_repository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Del_Sucess;

            return Resp_Binary.Del_Failed;
        }

        public Resp_Binary Import(File_Import request)
        {
            if (string.IsNullOrEmpty(request.FileData))
                return new Resp_Binary { message = "上传内容为空" };

            var fileExt = Utils.GetFileExt(request.FileName);
            if (!IsExcel(fileExt))
                return new Resp_Binary { message = "仅允许上传Excel文件" };

            var data = Convert.FromBase64String(request.FileData);
            var fileName = Utils.GetFileNameWithoutExt(request.FileName) + (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds + "." + fileExt;

            var baseUrl = System.AppDomain.CurrentDomain.BaseDirectory;
            var dirPath = baseUrl + "excel\\layout";
            var filePath = dirPath + "\\" + fileName;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            FileStream fs2 = new FileStream(filePath, FileMode.OpenOrCreate);
            fs2.Write(data, 0, data.Length);
            fs2.Flush();
            fs2.Close();

            return Import_Data(filePath, request.UserId);

        }

        public Resp_Binary Import_Data(string fileName, int userId)
        {

            var targetFile = new FileInfo(fileName);
            if (!targetFile.Exists)
                return new Resp_Binary { message = "导入的数据文件未保存成功" };

            var excelFile = new ExcelQueryFactory(fileName);
            excelFile.AddMapping<LayoutExModel>(x => x.CarNumber, "车牌号*");
            excelFile.AddMapping<LayoutExModel>(x => x.ValideTime, "有效期");
            excelFile.AddMapping<LayoutExModel>(x => x.Degree, "有效次数*");
            excelFile.AddMapping<LayoutExModel>(x => x.TriggerType, "进出场控制*");
            excelFile.AddMapping<LayoutExModel>(x => x.Description, "布控说明");

            DateTime? dtN = null;

            var excelContent = excelFile.Worksheet<LayoutExModel>(0);
            //var sheetList = excelFile.GetWorksheetNames();
            var layouts = new List<Layout>();
            foreach (var row in excelContent)
            {

                if (string.IsNullOrEmpty(row.CarNumber))
                {
                    return new Resp_Binary { message = "车牌号不允许为空" };
                }
                var layout = new Layout
                {
                    CarNumber = row.CarNumber,
                    ValideTime = DateTime.TryParse(row.ValideTime, out DateTime dt) ? dt : dtN,
                    SysUserId = userId,
                    Degree = Enum.TryParse(row.Degree, out Layout_Degree result) ? (int)result : 0,
                    TriggerType = Enum.TryParse(row.TriggerType, out Layout_TriggerType result2) ? (int)result2 : 0,
                    CreateTime = DateTime.Now,
                    IsValid = 1,
                    Description = row.Description

                };
                layouts.Add(layout);

            }
            BulkOperation<Layout>.MySqlBulkInsert(layouts, _repository);

            return new Resp_Binary { flag = 1, message = "导入成功" };
        }

        public Resp_Binary Modify(LayoutDTO model)
        {
            var entity = _repository.GetById(model.ID.ToInt());
            if (entity.IsNull())
                return new Resp_Binary { message = "未找到对应操作记录，请重新查询数据" };

            DateTime? dt = null;
            entity.TriggerType = model.TriggerType;
            entity.ValideTime = !string.IsNullOrEmpty(model.ValideTime) ? model.ValideTime.ToDateTime() : dt;
            entity.IsValid = model.IsValid;
            entity.Degree = model.Degree;
            entity.Description = model.Description;
            entity.SysUserId = model.SysUserId.ToInt();

            _repository.Update(entity);
            if (_repository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Modify_Sucess;
            return Resp_Binary.Modify_Failed;
        }

        private bool IsExcel(string _fileExt)
        {
            ArrayList al = new ArrayList();
            al.Add("xls");
            al.Add("xlsx");
            if (al.Contains(_fileExt.ToLower()))
            {
                return true;
            }
            return false;
        }

        public Resp_Query<LayoutDTO> Query(Layout_Query request)
        {
            var response = new Resp_Query<LayoutDTO>();
            var records = _repository.GetAll();

            records.ToMaybe()
                .Do(d => request.Verify())
                .DoWhen(t => !string.IsNullOrEmpty(request.CarNumber), d => records = records.Where(s => s.CarNumber.Contains(request.CarNumber)));

            if (!string.IsNullOrEmpty(request.Trigger) && int.TryParse(request.Trigger, out int result))
                records = records.Where(s => s.TriggerType == result);

            if (request.IsValid == 1)
                records = records.Where(s => s.IsValid == 1 && (s.ValideTime == null || s.ValideTime > DateTime.Now || s.ValideTime < DateTime.MinValue));

            if (request.IsValid == 0)
                records = records.Where(s => s.IsValid == 0 || (s.ValideTime != null && s.ValideTime < DateTime.Now && s.ValideTime > DateTime.MinValue));

            if (!string.IsNullOrEmpty(request.BeginTime) && DateTime.TryParse(request.BeginTime, out DateTime start))  //Linq to entity 不支持datatime.parse函数
            {
                records = records.Where(s => s.CreateTime >= start);
            }

            if (!string.IsNullOrEmpty(request.EndTime) && DateTime.TryParse(request.EndTime, out DateTime end))
            {
                records = records.Where(s => s.CreateTime <= end);
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

            response.entities = records.Skip(request.PgSize * (request.PgIndex - 1)).Take(request.PgSize).ToList().ConvertoDto<Layout, LayoutDTO>().ToList();
            return response;
        }

        public Resp_Binary Valid_Set(Layout_Valid_Set request)
        {
           if(request.Ids==null)
                throw new ArgumentNullException(nameof(request));


            Array.ForEach(request.Ids, id =>
            {
                var model = _repository.GetById(id);
                if (model.IsNotNull())
                {
                    model.IsValid = request.Valid;
                    model.CreateTime = DateTime.Now;
                }
            });

            _repository.UnitOfWork.Commite();

            return Resp_Binary.Modify_Sucess;
            
        }

        public Resp_Index<LayoutDTO> Index(Req_Index request)
        {
            var response = new Resp_Index<LayoutDTO>();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (limits.IsNotNull() && limits.Count > 0 && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits;
                var query_parameter = new Layout_Query { PgIndex = 1, PgSize = request.PgSize };
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

        public Resp_Temporary_Sync Temporary_Sync(Req_Temporary_Sync request)
        {
            var response = new Resp_Temporary_Sync { lastSyncTime = DateTime.Now.ToString() };
            var records = _repository.GetAll();

            if (!string.IsNullOrEmpty(request.lastSyncTime) && DateTime.TryParse(request.lastSyncTime, out DateTime start))  //Linq to entity 不支持datatime.parse函数
            {
                records = records.Where(s => s.CreateTime >= start);
            }

            if (!string.IsNullOrEmpty(response.lastSyncTime) && DateTime.TryParse(response.lastSyncTime, out DateTime end))
            {
                records = records.Where(s => s.CreateTime < end);
            }
            records = records.OrderBy(o => o.ID);
            response.entities = records.ToList();

            return response;
        }

        public Resp_All_Sync All_Sync(Req_All_Sync request)
        {
            var datas = new Resp_Query<Layout>();
            var pgSize = System.Configuration.ConfigurationManager.AppSettings["syncSize"].ToInt();
            var response = new Resp_All_Sync();
            response.lastSyncTime = DateTime.Now.ToString();
            var records = _repository.GetAll().OrderBy(o=>o.ID);
            datas.totalCounts = records.Count();
            datas.totalRows = (int)Math.Ceiling((double)datas.totalCounts / pgSize);
            if (request.PgIndex >= 1)
                datas.entities = records.Skip(pgSize * (request.PgIndex - 1)).Take(pgSize).ToList();
            else
                datas.entities = new List<Layout>();

            response.batchDatas = datas;
            response.done = request.PgIndex >= datas.totalRows;
            

            return response;
        }

        public Resp_CheckExsits<LayoutDTO> Exsits(Req_CheckExsits request)
        {
            var response = new Resp_CheckExsits<LayoutDTO>();
            var entity = _repository.GetByWhere(t => t.CarNumber == request.CarNumber).OrderByDescending(t => t.ID).FirstOrDefault();
            if (entity.IsNotNull())
            {
                if ((entity.ValideTime.IsNotNull() && entity.ValideTime < DateTime.Now && entity.ValideTime > DateTime.MinValue) || entity.IsValid == 0)
                {
                    response.flag = 2;
                    response.message = "该车牌曾已布控，且已失效，如需重新布控，请修改重新提交即可";
                }
                else
                {
                    response.flag = 1;
                    response.message = "该车牌已布控，请检查是否需要修改";
                }

                response.entity = entity.ConvertoDto<Layout, LayoutDTO>();
            }
            else
                response.message = "该车牌未曾布控";

            return response;
        }

        public Resp_Binary_Member<LayoutDTO> Hit(Layout_Hit request)
        {
            var layout = _repository.GetByWhere(d => d.CarNumber == request.CarNumber && d.IsValid == 1 && (d.ValideTime == null || d.ValideTime > DateTime.Now || d.ValideTime < DateTime.MinValue)).FirstOrDefault();
            if(layout.IsNotNull())
            {
                if (layout.TriggerType == request.Pass || layout.TriggerType == 2)  //triggerType=2代表进/出场都控制
                {
                    var dto = layout.ConvertoDto<Layout, LayoutDTO>();
                    return new Resp_Binary_Member<LayoutDTO> { message = "车辆已中控", flag = 1, entity = dto };
                }
            }

            return new Resp_Binary_Member<LayoutDTO> { message = "车辆未中控", flag = 0 };

        }
    }
}
