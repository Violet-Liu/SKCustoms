using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;
using Common;
using Domain;
using Services.Mapping;

namespace Services
{
    public class SysModuleService : ISysModuleService
    {
        [Dependency]
        public ISysModuleRepository _sysModuleRepository { get; set; }

        [Dependency]
        public ISysModuleOperateRepository _sysModuleOperateRepository { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }

        public Resp_Binary Add_Btn(SysModuleOperateDTO model)
        {
            var _model = model.GetPrototype<SysModuleOperateDTO, SysModuleOperate>();
            var entity = _sysModuleOperateRepository.GetByWhere(t => t.KeyCode == _model.KeyCode && t.ModuleId == _model.ModuleId).FirstOrDefault();
            if (entity.IsNotNull())
                return new Resp_Binary { message="操作码已存在"};

            _sysModuleOperateRepository.Insert(_model);
            if (_sysModuleOperateRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Add_Sucess;

            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Create(SysModuleDTO model)
        {
            var entity = model.GetPrototype<SysModuleDTO, SysModule>();
            if (model.ParentId < 1)
            {
                
                var sysModule = _sysModuleRepository.GetByWhere(t => t.Name == entity.Name).FirstOrDefault();
                if (sysModule.IsNotNull())
                    return new Resp_Binary { message = "菜单名已存在" };

                _sysModuleRepository.Insert(entity);    
            }
            else
            {
                var parent = _sysModuleRepository.GetById(model.ParentId);
                if (parent.IsNull())
                    return new Resp_Binary { message = "未找到父级菜单" };

                parent.SubSysModules.Add(entity);
            }
            if (_sysModuleRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Add_Sucess;
            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Del(Base_Del_Request request)
        {
            var entity = _sysModuleRepository.GetById(request.Id);
            if (entity.IsNull())
              return new Resp_Binary { message = "菜单不存在" };
            var sub_entities = entity.SubSysModules;
            if (sub_entities.Count() > 0)
                return new Resp_Binary { message = "删除失败,有下属关联，请先删除关联下属" };

            _sysModuleRepository.Delete(entity);

            if (_sysModuleRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Del_Sucess;

            return Resp_Binary.Del_Failed;

        }

        public Resp_Binary Del_Btn(Base_Del_Request request)
        {
            if (request.Id < 1 && _sysModuleOperateRepository.GetById(request.Id).IsNull())
                return new Resp_Binary { message = "请选择操作记录" };

            _sysModuleOperateRepository.DeleteById(request.Id);
            if (_sysModuleOperateRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Del_Sucess;

            return Resp_Binary.Del_Failed;

            
        }

        public Resp_Binary Modify(SysModuleDTO model)
        {
            var entity = model.GetPrototype<SysModuleDTO, SysModule>();
            if (entity.IsNull() && entity.ID < 1)
                return new Resp_Binary { message="请选择要操作的记录" };

            _sysModuleRepository.Update(entity);
            if (_sysModuleRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Modify_Sucess;

            return Resp_Binary.Modify_Failed;

        }

        public Resp_Query<SysModuleDTO> Query(SysMoudule_Query request)
        {
            var response = new Resp_Query<SysModuleDTO>();
            List<SysModule> recodes = null;
            //recodes = _sysModuleRepository.GetSysModules(request.ParentId).ToList();
            if (string.IsNullOrEmpty(request.ParentId))
                recodes = _sysModuleRepository.GetByWhere(t => t.SuperSysModule == null).ToList();
            else
            {
                if (int.TryParse(request.ParentId, out int result))
                    recodes = _sysModuleRepository.GetByWhere(t => t.SuperSysModule.ID == result).ToList();
            }

            recodes.ForEach(t =>
            {
                if(t.SubSysModules.IsNullOrEmpty())
                {
                    t.IsLast = 1;
                }
            });

            response.entities = recodes.IsNotNull() ? recodes.ConvertoDto<SysModule, SysModuleDTO>().ToList() : new List<SysModuleDTO>();

            response.totalCounts = response.entities.Count();
            response.totalRows = 1;

            return response;
        }

        public Resp_Index<SysModuleDTO> Index(Req_Index request)
        {
            var response = new Resp_Index<SysModuleDTO>();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (!limits.IsNullOrEmpty() && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits.OrderByDescending(t => t.IsValid).GroupBy(t => new { t.KeyCode, t.KeyName }).Select(s => new SysModuleOperateIndexDTO
                {
                    KeyCode = s.Key.KeyCode,
                    KeyName = s.Key.KeyName,
                    IsValid = s.Sum(x => x.IsValid),
                }).ToList();
                var query_parameter = new SysMoudule_Query { ParentId = "" };
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
    }
}
