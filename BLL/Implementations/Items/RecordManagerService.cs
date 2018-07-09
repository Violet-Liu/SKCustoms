using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Unity.Attributes;
using Common;
using Infrastructure;
using Services.Mapping;
using System.IO;
using LinqToExcel;
using System.Collections;
using Repostories;

namespace Services
{
    public class RecordManagerService : IRecordManagerService
    {
        [Dependency]
        public IRecordManagerRepository _repository { get; set; }

        [Dependency]
        public ISysErrorLogRepository _error { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }

        [Dependency]
        public ISysUserRepository _sysUserRepository { get; set; }

        public Resp_Query<RecordManagerDTO> Query(RecordManager_Query request)
        {

            var response = new Resp_Query<RecordManagerDTO>();
            var records = _repository.GetAll();

            records.ToMaybe()
                .Do(d => request.Verify())
                .DoWhen(t => !string.IsNullOrEmpty(request.CarNumber.NullToString().Trim()), d => records = records.Where(s => s.CarNumber.Contains(request.CarNumber)))
                .DoWhen(t => !string.IsNullOrEmpty(request.DLicense.NullToString().Trim()), d => records = records.Where(s => s.DLicense.Contains(request.DLicense)))
                .DoWhen(t => !string.IsNullOrEmpty(request.TLicense.NullToString().Trim()), d => records = records.Where(s => s.TLicense.Contains(request.TLicense)))
                .DoWhen(t => !string.IsNullOrEmpty(request.Driver.NullToString().Trim()), d => records = records.Where(s => s.Driver.Contains(request.Driver)))
                .DoWhen(t => !string.IsNullOrEmpty(request.Contact.NullToString().Trim()), d => records = records.Where(s => s.Contact.Contains(request.Contact)))
                .DoWhen(t => !string.IsNullOrEmpty(request.RecordMGrade.NullToString().Trim()), d => records = records.Where(s => s.RecordMGrade.Equals(request.RecordMGrade)))
                .DoWhen(t => !string.IsNullOrEmpty(request.CarColor.NullToString().Trim()), d => records = records.Where(s => s.CarColor.Equals(request.CarColor)))
                .DoWhen(t => !string.IsNullOrEmpty(request.CarType.NullToString().Trim()), d => records = records.Where(s => s.CarType.Equals(request.CarType)))
                .DoWhen(t => !string.IsNullOrEmpty(request.Channel.NullToString().Trim()), d => records = records.Where(s => request.Channel.Contains(s.Channel)));

            if (!string.IsNullOrEmpty(request.IsValid) && int.TryParse(request.IsValid, out int result))
            {
                if (result == 0)
                    records = records.Where(s => s.IsValid == 0);
                else if (result == 1)
                    records = records.Where(s => s.IsValid == 1);
            }
            if(!string.IsNullOrEmpty(request.BeginTime) && DateTime.TryParse(request.BeginTime, out DateTime start))  //Linq to entity 不支持datatime.parse函数
                records = records.Where(s => s.CreateTime >= start);

            if (!string.IsNullOrEmpty(request.EndTime) && DateTime.TryParse(request.EndTime, out DateTime end))
                records = records.Where(s => s.CreateTime <= end);

            response.totalCounts = records.Count();
            response.totalRows = (int)Math.Ceiling((double)response.totalCounts / request.PgSize);

            if (!string.IsNullOrEmpty(request.Order))
                records = records.DataSorting(request.Order, request.Esc);
            else
                records = records.OrderByDescending(t => t.ID);

            response.entities = records.Skip(request.PgSize * (request.PgIndex - 1)).Take(request.PgSize).ToList().ConvertoDto<RecordManager, RecordManagerDTO>().ToList();
            return response;
                
        }



        public Resp_CheckExsits<RecordManagerDTO> Exsits(Req_CheckExsits request)
        {
            var response = new Resp_CheckExsits<RecordManagerDTO>();
            var entity = _repository.GetByWhere(t => t.CarNumber == request.CarNumber && t.Channel == request.Channel).OrderByDescending(t => t.ID).FirstOrDefault();
            if (entity.IsNotNull())
            {
                if (entity.IsValid == 0)
                {
                    response.flag = 2;
                    response.message = "该车牌曾已备案，且已失效，如需重新备案，请修改重新提交即可";
                }
                else
                {
                    response.flag = 1;
                    response.message = "该车牌已备案，请检查是否需要修改";
                }

                response.entity = entity.ConvertoDto<RecordManager, RecordManagerDTO>();
            } else
                response.message = "该车牌未曾备案";

            return response;
        }

        /// <summary>
        /// 一次增加多个港口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Resp_Binary Add(Req_RecordManager_Add request)
        {
            var record = request.entity.GetPrototype<RecordManagerDTO, RecordManager>();
            record.IsValid = 1;

            if (!request.channels.IsNullOrEmpty())
            {
                request.channels.ForEach(s =>
                {
                    var temp = record.Copy();
                    temp.Channel = s;
                    _repository.Insert(temp);
                });
            }

            if (_repository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Add_Sucess;
            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Add_One(RecordManagerDTO model)
        {
            var old_model = _repository.GetByWhere(t => t.CarNumber == model.CarNumber && t.Channel == model.Channel && t.IsValid == 1).FirstOrDefault();
            if (old_model.IsNotNull())
                return Resp_Binary.Add_Repeated; 

            var record = model.GetPrototype<RecordManagerDTO, RecordManager>();
            record.IsValid = 1;
            _repository.Insert(record);
            if (_repository.UnitOfWork.Commite()>0)
                return Resp_Binary.Add_Sucess;
            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Add_Batch(IEnumerable<RecordManagerDTO> models)
        {
            if (models.Count() < 5000)
                BulkOperation<RecordManager>.MySqlBulkInsert(models.GetPrototype<RecordManagerDTO, RecordManager>(),_repository);
            else
                BulkOperation<RecordManager>.MySqlBulkInsertAsync(models.GetPrototype<RecordManagerDTO, RecordManager>(),_repository);


            _repository.DelRepeatRecord();
            return Resp_Binary.Add_Sucess;
        }


        public Resp_Binary Del_Batch(IEnumerable<int> ids)
        {
            if (ids.IsNotNull() && ids.Count() > 0)
            {
                _repository.Delete(ids);
                if (_repository.UnitOfWork.Commite() > 0)
                    return Resp_Binary.Del_Sucess;

            }
            return Resp_Binary.Del_Failed;
        }

        public Resp_Binary Modify(RecordManagerDTO model)
        {
            var entity = _repository.GetById(model.ID.ToInt());
            if (entity.IsNull())
                return new Resp_Binary { message = "未找到对应操作记录，请重新查询数据" };

            DateTime? dt = null;
            entity.CarType = model.CarType;
            entity.CarColor = model.CarColor;
            entity.Type = model.Type.ToInt();
            entity.TLicense = model.TLicense;
            entity.DLicense = model.DLicense;
            entity.Driver = model.Driver;
            entity.Channel = model.Channel;
            entity.Contact = model.Contact;
            entity.Organization = model.Organization;
            entity.SysUserId = model.SysUserId.ToInt();
            entity.CreateTime = DateTime.Now;
            entity.ValideTime = string.IsNullOrEmpty(model.ValideTime) ? dt : model.ValideTime.ToDateTime();
            entity.RecordMGrade = model.RecordMGrade;
           
            _repository.Update(entity);
            if(_repository.UnitOfWork.Commite()>0)
                return  Resp_Binary.Modify_Sucess;
            return Resp_Binary.Modify_Failed;
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
            var dirPath = baseUrl + "excel\\recordmanage";
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
            excelFile.AddMapping<RecordManager>(x => x.CarNumber, "车牌号*");
            excelFile.AddMapping<RecordManager>(x => x.Channel, "行政通道*");
            excelFile.AddMapping<RecordManager>(x => x.TLicense, "行驶证号*");
            excelFile.AddMapping<RecordManager>(x => x.DLicense, "驾驶证号*");
            excelFile.AddMapping<RecordManager>(x => x.RecordMGrade, "备案分级");
            excelFile.AddMapping<RecordManager>(x => x.ValideTime, "有效期");
            excelFile.AddMapping<RecordManager>(x => x.CarColor, "车身颜色");
            excelFile.AddMapping<RecordManager>(x => x.CarType, "车类型");
            excelFile.AddMapping<RecordManager>(x => x.Driver, "驾驶人");
            excelFile.AddMapping<RecordManager>(x => x.Contact, "联系方式");
            excelFile.AddMapping<RecordManager>(x => x.Organization, "组织单位");

            var excelContent = excelFile.Worksheet<RecordManager>(0);
            var records = new List<RecordManager>();
            if (excelContent.IsNotNull() && excelContent.Count() > 0)
            {
                foreach(var row in excelContent)
                { 

                    if (string.IsNullOrEmpty(row.CarNumber))
                    {
                        return new Resp_Binary { message = "车牌号不允许为空" };
                    }

                    if (string.IsNullOrEmpty(row.TLicense))
                    {
                        return new Resp_Binary { message = "行驶证号不允许为空" };
                    }

                    if (string.IsNullOrEmpty(row.DLicense))
                    {
                        return new Resp_Binary { message = "驾驶证号不允许为空" };
                    }
       
                    var record = new RecordManager
                    {
                        IsValid=1,
                        CarNumber = row.CarNumber,
                        Channel=row.Channel,
                        TLicense = row.TLicense,
                        DLicense = row.DLicense,
                        RecordMGrade = row.RecordMGrade,
                        ValideTime=row.ValideTime,
                        CarColor = row.CarColor,
                        CarType = row.CarType,
                        Driver = row.Driver,
                        Contact = row.Contact,
                        Organization = row.Organization,
                        SysUserId = userId,
                        CreateTime = DateTime.Now,
                    };
                    records.Add(record);
                }

            }
            BulkOperation<RecordManager>.MySqlBulkInsert(records, _repository);

            //_repository.DelRepeatRecord();
            return new Resp_Binary { flag = 1, message = "导入成功" };
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

        public Resp_Index<RecordManagerDTO> Index(Req_Index request)
        {
            var response = new Resp_Index<RecordManagerDTO>();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (limits.IsNotNull() && limits.Count > 0 && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits;
                var query_parameter = new RecordManager_Query { PgIndex = 1, PgSize = request.PgSize };
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

        public int JobSetInValid()
        {
           return _repository.SetInValid();
        }
    }
}
