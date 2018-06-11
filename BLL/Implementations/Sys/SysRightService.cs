using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Unity.Attributes;
using Common;
using Services.Mapping;
using Repostories;

namespace Services
{
    public partial class SysRightService : ISysRightService
    {
        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }
        [Dependency]
        public ISysModuleRepository _sysModuleRepository { get; set; }

        [Dependency]
        public ISysRoleRepository _sysRoleRepository { get; set; }

        [Dependency]
        public ISysUserRepository _sysUserRepository { get; set; }


        public Resp_Query<SysRightViewModel> GetRightByRole(SysRightGetByUser request)
        {
            var d = _sysRightRepository.GetRightByRole(request.RoleId);
            var datas = d.ToList();
            return GetRightsByCondition(datas, request);

        }

        public Resp_Query<SysRightViewModel> GetRightByUser(SysRightGetByUser request)
        {
            var d = _sysRightRepository.GetRightByUser(request.UserId);
            var datas = d.ToList();
            return GetRightsByCondition(datas, request);
        }

        private string GetModuleName(List<SysModule> modules, int SysModule_ID, string ModuleName)
        {
            var temp = modules.Find(t => t.ID == SysModule_ID);
            if (temp.IsNotNull())
            {
                if (temp.SuperSysModule == null)
                    ModuleName= temp.Name + " " + ModuleName;
                else
                    GetModuleName(modules, temp.SuperSysModule.ID, ModuleName);
            }

            return ModuleName;
        }

        private Resp_Query<SysRightViewModel> GetRightsByCondition(List<SysUserRightView> datas, SysRightGetByUser request)
        {
            var response = new Resp_Query<SysRightViewModel>();
            var views = new List<SysRightViewModel>();
            if (datas.IsNotNull() && datas.Count > 0)
            {
                var groups = datas.GroupBy(t => new { t.ModuleName, t.SysModuleId, t.ParentId });

                var sysModules = Cache.GetSysModules();
                foreach (var group in groups)
                {
                    var view = new SysRightViewModel();
                    view.ModuleName = GetModuleName(sysModules, group.Key.ParentId, group.Key.ModuleName);//group.Key.ModuleName;
                    view.SysModuleId = group.Key.SysModuleId;
                    view.ParentId = group.Key.ParentId;
                    foreach (var temp in group)
                    {
                        view.Operaties.Add(new SysRightOperateViewModel { KeyCode = temp.KeyCode, Name = temp.Name, IsValid = temp.IsValid });
                    }

                    views.Add(view);
                }
                response.entities = views.Skip((request.PgIndex - 1) * request.PgSize).Take(request.PgSize).ToList();
                response.totalCounts = views.Count;
                response.totalRows = (int)Math.Ceiling((double)response.totalCounts / request.PgSize);
            }
            return response;
        }


        public List<SysModule2DTO> GetRightModuleByUser(int userId)
        {
            var resp_datas = new List<SysModule2DTO>();
            var datas = new List<SysModule>();
            if (userId == 1)
                datas = _sysModuleRepository.GetAll().ToList();
            else
            {
                var sysUser = _sysUserRepository.GetById(userId);
                if(sysUser.SysRoles.IsNotNull())
                {
                    foreach(var role in sysUser.SysRoles)
                    {
                        if(role.IsNotNull())
                        {
                            foreach (var sysRight in role.SysRights)
                            {
                                if (sysRight.IsNotNull() && sysRight.SysModule.IsNotNull() && sysRight.Rightflag == 1)
                                {
                                    if (datas.Find(t => t.ID == sysRight.SysModule.ID).IsNull())
                                        datas.Add(sysRight.SysModule);
                                }
                            }
                        }
                    }
                }
                //datas = _sysModuleRepository.GetSysModulesByUserId(userId).ToList();
                //if (datas.IsNotNull() && datas.Count > 0)
                //    datas = datas.Select(t => _sysModuleRepository.GetById(t.ID)).ToList();
            }
                
            if(datas.IsNotNull()&&datas.Count>0)
            {
                var firstMenu = datas.Where(t => t.SuperSysModule == null).ToList();
                if(firstMenu.IsNotNull()&&firstMenu.Count>0)
                {
                    firstMenu.ForEach(t => {
                        resp_datas.Add(Transform(datas, t));
                    });
                }   
            }

            return resp_datas;
        }

        /// <summary>
        /// 转化菜单
        /// </summary>
        /// <param name="datas">有权限的菜单集合</param>
        /// <param name="module">待转化以及校验的菜单</param>
        /// <returns></returns>
        private SysModule2DTO Transform(List<SysModule> datas,SysModule module)
        {
            if (module != null)
            {
                var intersect = module.SubSysModules.Intersect(datas);
                var temp = module.ConvertoDto<SysModule, SysModule2DTO>();
                if (intersect.IsNotNull() && intersect.Count() > 0)
                {
                    foreach (var sub in intersect)
                    {
                        var recursionModel = Transform(datas, sub);
                        if (recursionModel != null)
                        {
                            temp.SubSysModules.Add(recursionModel);
                        }
                        else
                        {
                            temp.IsLast = 1;
                        }
                    }

                }
                return temp;
            }
            return null;
        }

        /// <summary>
        /// cs端获取是否拥有修改报警信息
        /// </summary>
        /// <param name="u_name"></param>
        /// <returns></returns>
        public Resp_Binary GetAlarmCheckRight(string u_name)
        {
            if (string.IsNullOrEmpty(u_name))
                return new Resp_Binary { message = "用户名参数不允许为空" };

            var sysUser = _sysUserRepository.GetByWhere(t => t.Name == u_name).FirstOrDefault();
            if (sysUser.IsNull())
                return new Resp_Binary { message = "未找到用户信息" };

            var right = _sysRightRepository.GetRightByUserWithModule(sysUser.ID, "报警检查", "Modify");
            if (right.IsNullOrEmpty() || right.Where(t => t.IsValid == 1).FirstOrDefault().IsNull())
                return new Resp_Binary { message = "该用户无修改报警信息权限" };
            else
                return new Resp_Binary { flag = 1 };

        }
    }
}
