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

namespace Services
{
    public class CarTypeService : ICarTypeService
    {
        [Dependency]
        public ICarTypeRepository _carTypeRepository { get; set; }
        
        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }

        public Resp_Binary Add(CarType model)
        {
            if (_carTypeRepository.GetByWhere(t => t.Name == model.Name.Trim()).FirstOrDefault().IsNotNull())
                return new Resp_Binary { message = "类型名称重复" };

            _carTypeRepository.Insert(model);
            if (_carTypeRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Add_Sucess;
            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Del(Base_SingleDel model)
        {
            if (model.Id == 0)
                return new Resp_Binary { message = "请选择要操作的记录" };

            _carTypeRepository.DeleteById(model.Id);
            if (_carTypeRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Del_Sucess;

            return Resp_Binary.Del_Failed;
        }

        public Resp_Index<CarType> Index(Req_Index request)
        {
            var response = new Resp_Index<CarType>();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (!limits.IsNullOrEmpty() && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits;
                var query_parameter = new RMG_Query { PgIndex = 1, PgSize = request.PgSize };
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

        public Resp_Binary Modify(CarType model)
        {
            if (model.IsNull())
                return new Resp_Binary { message = "请选择要操作的记录" };

            var record = _carTypeRepository.GetById(model.ID);
            if (record.IsNull())
                return new Resp_Binary { message = "未找到记录信息，请刷新页面" };

            record.Name = model.Name;

            _carTypeRepository.Update(record);
            if (_carTypeRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Modify_Sucess;

            return Resp_Binary.Modify_Failed;
        }

        public Resp_Query<CarType> Query(RMG_Query request)
        {
            var response = new Resp_Query<CarType>();

            var records = _carTypeRepository.GetAll();

            records.ToMaybe()
                .Do(t => request.Verify())
                .DoWhen(t => !string.IsNullOrEmpty(request.QueryStr), d => records = records.Where(s => s.Name.Contains(request.QueryStr)));
            response.totalCounts = records.Count();
            response.totalRows = (int)Math.Ceiling((double)response.totalCounts / request.PgSize);

            if (!string.IsNullOrEmpty(request.Order))
            {
                records = records.DataSorting(request.Order, request.Esc);
            }
            else
            {
                records = records.OrderBy(t => t.ID);
            }
            var entities = records.Skip(request.PgSize * (request.PgIndex - 1)).Take(request.PgSize).ToList();
            response.entities = entities;

            return response;
        }
    }
}
