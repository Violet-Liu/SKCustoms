using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Mapping;
using Unity.Attributes;
using Domain;
using Common;
using Repostories;

namespace Services
{
    public class SysRightOperatorService : ISysRightOperatorService
    {
        [Dependency]
        public ISysRightOperateRepository _sysRightOperateRepository { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }

        [Dependency]
        public ISysModuleRepository _sysModuleRepository { get; set; }

        [Dependency]
        public ISysModuleOperateRepository _sysModuleOperateRepository { get; set; }

        public Resp_Binary UpdateRight(SysRightOperate_Update request)
        {
            if (request.IsNull() && request.ModuleOperateIds.Count < 1)
                return new Resp_Binary { message = "请选择要操作的记录" };

            if (request.ModuleId < 1)
                return new Resp_Binary { message = "请选择一个模块" };

            if (request.RoleId < 1)
                return new Resp_Binary { message = "请选择一个角色" };


            var sysRight = _sysRightRepository.GetByWhere(r => r.SysRoleId == request.RoleId && r.SysModuleId == request.ModuleId).FirstOrDefault();

            if (sysRight.IsNull())
            {
                if (request.ModuleOperateIds.Exists(t => t.IsRightValid == 1))
                    sysRight = new SysRight { SysModuleId = request.ModuleId, SysRoleId = request.RoleId, Rightflag = 1 };
                else
                    sysRight = new SysRight { SysModuleId = request.ModuleId, SysRoleId = request.RoleId, Rightflag = 0 };

                _sysRightRepository.Insert(sysRight);
            }
            else
            {
                using (var context = new SKContext())
                {
                    var delsysRight = context.SysRights.Single(t => t.ID == sysRight.ID);
                    delsysRight.SysRightOperates.Clear();
                    var temp = context.SysRightOperates.Local.Where(p => p.SysRight == null).ToList();
                    temp.ForEach(r => context.SysRightOperates.Remove(r));
                    var s = context.SaveChanges();
                }
            }


            request.ModuleOperateIds.ForEach(t =>
            {
                var sysRightOperate = new SysRightOperate { RightId = sysRight.ID, SysModuleOperateId = t.ID, IsValid = t.IsRightValid };
                sysRight.SysRightOperates.Add(sysRightOperate);
            });
            
            var count = _sysRightRepository.UnitOfWork.Commite();
            if (count > 0)
            {
                var sysRightAfter= _sysRightRepository.GetByWhere(r => r.SysRoleId == request.RoleId && r.SysModuleId == request.ModuleId).FirstOrDefault();
                if (sysRightAfter.SysRightOperates.FirstOrDefault(ss => ss.IsValid == 1).IsNull())       //更新用户权限
                {
                    sysRightAfter.Rightflag = 0;
                    _sysRightRepository.Update(sysRightAfter);
                }
                else
                {
                    sysRightAfter.Rightflag = 1;
                    _sysRightRepository.Update(sysRightAfter);      
                }

                var module = _sysModuleRepository.GetByWhere(r => r.ID == request.ModuleId).FirstOrDefault();
                UpateParentModuleRight(module);
                void UpateParentModuleRight(SysModule source)
                {
                    if (source.IsNotNull())
                    {
                        var parent = source.SuperSysModule;
                        if (parent.IsNotNull())
                        {
                            var sysSuperRight = _sysRightRepository.GetByWhere(r => r.SysRoleId == request.RoleId && r.SysModuleId == parent.ID).FirstOrDefault();
                            var sysSourceRights = _sysRightRepository.GetByWhere(r => r.SysRoleId == request.RoleId && r.Rightflag == 1);
                            var _Rightflag = 0;
                            if (!sysSourceRights.IsNullOrEmpty())
                                _Rightflag = 1;
                            if (sysSuperRight.IsNull())
                            {
                                sysRight = new SysRight { SysModuleId = parent.ID, SysRoleId = request.RoleId, Rightflag = _Rightflag };
                                _sysRightRepository.Insert(sysRight);
                            }
                            else
                            {
                                sysSuperRight.Rightflag = _Rightflag;
                                _sysRightRepository.Update(sysSuperRight);
                            }
                            UpateParentModuleRight(parent);
                        }
                    }
                }

                _sysRightRepository.UnitOfWork.Commite();
                return Resp_Binary.Modify_Sucess;
            }
            return Resp_Binary.Modify_Failed;

        }


        public Resp_SysModuleOperate GetRightOperates(SysRightOperate_Get request)
        {
            
            if (request.ModuleId < 1) 
                return new Resp_SysModuleOperate { message = "请选择一个模块" };

            if (request.RoleId < 1)
                return new Resp_SysModuleOperate { message = "请选择一个角色" };

            var Resp_SysModuleOperate = new Resp_SysModuleOperate { flag=1,datas = new Resp_Query<SysModuleOperateDTO>() { entities = new List<SysModuleOperateDTO>() } };

            var sysRight = _sysRightRepository.GetByWhere(t => t.SysModuleId == request.ModuleId && t.SysRoleId == request.RoleId).OrderByDescending(t => t.ID).FirstOrDefault();

            var moduleOperates = _sysModuleOperateRepository.GetByWhere(t => t.ModuleId == request.ModuleId && t.IsValid == 1).OrderBy(t => t.Sort).ToList();
            if (sysRight.IsNotNull())
            {
                if (!moduleOperates.IsNullOrEmpty())
                {
                    moduleOperates.ForEach(d => {
                        var dto = d.ConvertoDto<SysModuleOperate, SysModuleOperateDTO>();
                        var temp = d.SysRightOperates.FirstOrDefault(t => t.RightId == sysRight.ID);
                        if(temp.IsNotNull())
                        {
                            dto.SysRightOperateId = temp.ID;
                            dto.SysRightId = temp.RightId;
                            dto.SysModuleOperateId = temp.SysModuleOperateId;
                            dto.IsRightValid = temp.IsValid;
                        }


                        Resp_SysModuleOperate.datas.entities.Add(dto);
                    });
                }
            }
            else
            {
                Resp_SysModuleOperate.datas.entities = moduleOperates.ConvertoDto<SysModuleOperate, SysModuleOperateDTO>().ToList();
            }

            return Resp_SysModuleOperate;
        }

        public Resp_RightOperator_Index Index(Req_Index request)
        {
            var response = new Resp_RightOperator_Index();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (!limits.IsNullOrEmpty() && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits;
                using (var context = new SKContext())
                {
                    response.sysRoles = context.SysRoles.ToList().ConvertoDto<SysRole, SysRoleDTO>().ToList();
                    response.sysModules = context.SysModules.Where(t => t.SuperSysModule == null).ToList().ConvertoDto<SysModule, SysModuleDTO>().ToList();
                }
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
