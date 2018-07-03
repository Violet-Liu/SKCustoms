using Common;
using Domain;
using Infrastructure;
using Repostories;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace Services
{
    public class SysRoleService : ISysRoleService
    {
        [Dependency]
        public ISysRoleRepository _sysRoleRepository { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }

        public Resp_Binary Assign_User(SysRole_Assign_Users request)
        {
            using (var context = new SKContext())
            {
                var sysRole = context.SysRoles.Where(t => t.ID == request.RoleId).FirstOrDefault();
                if (sysRole.IsNull())
                    return new Resp_Binary { message = "用户不存在" };
                sysRole.SysUsers.Clear();
                if (request.UserIds.IsNotNull() && request.UserIds.Count > 0)
                {
                    MemberInfo member1 = typeof(SysUser).GetProperty("ID");
                    ParameterExpression param = Expression.Parameter(typeof(SysUser), "o");
                    MemberExpression memberex1 = Expression.MakeMemberAccess(param, member1);

                    var ss = Expression.Equal(Expression.Constant(1), Expression.Constant(2));

                    foreach (var temp in request.UserIds)
                    {
                        var right = Expression.Equal(memberex1, Expression.Constant(temp));
                        ss = Expression.OrElse(ss, right);
                    }

                    var sysUsers = context.SysUsers.Where(Expression.Lambda<Func<SysUser, bool>>(ss, param)).ToList();

                    sysRole.SysUsers = sysUsers;

                }
                if (context.SaveChanges() > 0)
                    return Resp_Binary.Modify_Sucess;
            }
            return Resp_Binary.Modify_Failed;
        }

        public Resp_Binary Create(SysRoleDTO model)
        {
            var sysrole = model.GetPrototype<SysRoleDTO, SysRole>();
            if (_sysRoleRepository.GetByWhere(t => t.Name == sysrole.Name).FirstOrDefault().IsNotNull())
                return new Resp_Binary { message = "角色名已存在" };
            _sysRoleRepository.Insert(sysrole);
            if (_sysRoleRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Add_Sucess;

            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Del(Base_Del_Request request)
        {
            if(request.Ids.IsNotNull()&&request.Ids.Count>0)
            {
                if (request.Ids.Contains(1))
                    return new Resp_Binary { message = "系统角色不允许删除" };

                _sysRoleRepository.Delete(request.Ids);
                if (_sysRoleRepository.UnitOfWork.Commite() > 0)
                    return Resp_Binary.Del_Sucess;
            }
            else
            {
                return new Resp_Binary { message = "请选择要操作的记录" };
            }

            return Resp_Binary.Del_Failed;
        }

        public Resp_Binary Modify(SysRoleDTO model)
        {
            var sysrole = model.GetPrototype<SysRoleDTO,SysRole>();
            var entity = _sysRoleRepository.GetById(sysrole.ID);
            if (entity == null)
                return new Resp_Binary { message = "用户角色不存在" };

            entity.Name = sysrole.Name;
            entity.Description = sysrole.Description;
            entity.CreateTime = sysrole.CreateTime;
            entity.Creater = sysrole.Creater;

            _sysRoleRepository.Update(entity);
            if (_sysRoleRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Modify_Sucess;

            return Resp_Binary.Modify_Failed;
        }

        public Resp_Query<SysRoleDTO> Query(SysRole_Query request)
        {
            var response = new Resp_Query<SysRoleDTO>();

            var records = _sysRoleRepository.GetAll();

            records.ToMaybe()
                .Do(d => request.Verify())
                .Do(d => records = records.Where(s => s.ID > 1))   //系统角色不展现
                .DoWhen(t => !string.IsNullOrEmpty(request.QueryStr), d => records = records.Where(s => s.Name.Contains(request.QueryStr)));

            response.totalCounts = records.Count();
            response.totalRows = (int)Math.Ceiling((double)response.totalCounts / request.PgSize);

            if (!string.IsNullOrEmpty(request.Order))
            {
                records = records.DataSorting(request.Order, request.Esc);
            }
            else
            {
                records = records.OrderByDescending(t => t.ID);
            }
            var entities = records.Skip(request.PgSize * (request.PgIndex - 1)).Take(request.PgSize).ToList();
            response.entities = entities.ConvertoDto<SysRole, SysRoleDTO>().ToList();
            response.entities.ForEach(f => {
                var temp = entities.Find(t => t.ID == f.ID.ToInt());
                var UserName = new StringBuilder();
                if (temp.IsNotNull())
                {
                    temp.SysUsers.ToList().ForEach(s => UserName.Append("[" + s.Name + "]"));
                }
                f.UserName = UserName.ToString();
            });
            return response;
        }


        public Resp_Index<SysRoleDTO> Index(Req_Index request)
        {
            var response = new Resp_Index<SysRoleDTO>();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (limits.IsNotNull() && limits.Count > 0 && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits;
                var query_parameter = new SysRole_Query { PgIndex = 1, PgSize = request.PgSize };
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
