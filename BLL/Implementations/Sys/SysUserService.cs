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
using System.Linq.Expressions;
using System.Reflection;
using Repostories;

namespace Services
{
    public class SysUserService : ISysUserService
    {
        [Dependency]
        public ISysUserRepository _sysUserRepository { get; set; }

        [Dependency]
        public ISysRoleRepository _sysRoleRepository { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }

        public Resp_Binary Assign_Role(SysUser_Assign_Roles request)
        {
            using (var context = new SKContext())
            {
                var user = context.SysUsers.Where(t => t.ID == request.UserId).FirstOrDefault();
                if (user.IsNull())
                    return new Resp_Binary { message = "用户不存在" };
                user.SysRoles.Clear();
                if (request.RoleIds.IsNotNull() && request.RoleIds.Count > 0)
                {
                    MemberInfo member1 = typeof(SysRole).GetProperty("ID");
                    ParameterExpression param = Expression.Parameter(typeof(SysRole), "o");
                    MemberExpression memberex1 = Expression.MakeMemberAccess(param, member1);

                    var ss = Expression.Equal(Expression.Constant(1), Expression.Constant(2));

                    foreach (var temp in request.RoleIds)
                    {
                        var right = Expression.Equal(memberex1, Expression.Constant(temp));
                        ss = Expression.OrElse(ss, right);
                    }

                    var sysRoles = context.SysRoles.Where(Expression.Lambda<Func<SysRole, bool>>(ss, param)).ToList();

                    user.SysRoles = sysRoles;

                    if (context.SaveChanges() > 0)
                        return Resp_Binary.Modify_Sucess;

                }
            }
            return Resp_Binary.Modify_Failed;

        }

        public Resp_Binary Create(SysUserDTO model)
        {
            var sysuser = model.GetPrototype<SysUserDTO, SysUser>();
            var exist = _sysUserRepository.GetByWhere(s => s.Name == model.Name).FirstOrDefault();
            if(exist.IsNotNull())
            {
                return new Resp_Binary { message="用户名已存在"};
            }

            _sysUserRepository.Insert(sysuser);
            if(_sysUserRepository.UnitOfWork.Commite()>0)
            {
                return Resp_Binary.Add_Sucess;
            }
            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Del(Base_Del_Request request)
        {
            if (request.Ids.IsNotNull() && request.Ids.Count() > 0)
            {
                if(request.Ids.Contains(1))  //系统自生成的管理员
                {
                    return new Resp_Binary { message = "系统管理员不能被删除" };
                }
                _sysUserRepository.Delete(request.Ids);
                if (_sysUserRepository.UnitOfWork.Commite() > 0)
                {
                    return Resp_Binary.Del_Sucess;
                }
            }

            return Resp_Binary.Del_Failed;
        }

        public Resp_Binary Modify(SysUserDTO model)
        {
            var sysuser = model.GetPrototype<SysUserDTO, SysUser>();
            var exist = _sysUserRepository.GetById(sysuser.ID);
            if(exist.IsNull())
            {
                return new Resp_Binary { message = "用户不存在" };
            }

            exist.Pwd = sysuser.Pwd;
            exist.Position = sysuser.Position;
            exist.TrueName = sysuser.TrueName;
            exist.State = sysuser.State;
            exist.Sex = sysuser.Sex;
            exist.Remark = sysuser.Remark;
            exist.Contact = sysuser.Contact;
            exist.CreateTime = sysuser.CreateTime;

            _sysUserRepository.Update(exist);
            if(_sysUserRepository.UnitOfWork.Commite()>0)
            {
                return Resp_Binary.Modify_Sucess;
            }

            return Resp_Binary.Modify_Failed;
        }

        public Resp_Query<SysUserDTO> Query(SysUser_Query request)
        {
            var response = new Resp_Query<SysUserDTO>();

            var records = _sysUserRepository.GetAll();

            records.ToMaybe()
                .Do(d => request.Verify())
                .Do(d => records = records.Where(s => s.ID > 1))    //系统管理员不展示
                .DoWhen(t => !string.IsNullOrEmpty(request.QueryStr), d => records = records.Where(s => s.Name.Contains(request.QueryStr)||s.TrueName.Contains(request.QueryStr)));
                
            if(!string.IsNullOrEmpty(request.State)&&int.TryParse(request.State,out int result))
            {
                records = records.Where(s => s.State == result);
            }

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
            response.entities = entities.ConvertoDto<SysUser, SysUserDTO>().ToList();
            response.entities.ForEach(f => {
                var temp = entities.Find(t => t.ID == f.ID.ToInt());
                var RoleName = new StringBuilder();
                if (temp.IsNotNull())
                {
                    temp.SysRoles.ToList().ForEach(s => RoleName.Append("[" + s.Name + "]"));
                }
                f.RoleName = RoleName.ToString();
            });
            return response;

        }

        public Resp_Binary Reset_Pwd(SysUser_Reset_Pwd request)
        {
            var entity = _sysUserRepository.GetById(request.Id);

            if (entity.IsNull())
                return new Resp_Binary { message = "用户不存在" };

            entity.Pwd = request.Pwd;

            _sysUserRepository.Update(entity);

            if (_sysUserRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Modify_Sucess;

            return Resp_Binary.Modify_Failed;
        }

        public Resp_Index<SysUserDTO> Index(Req_Index request)
        {
            var response = new Resp_Index<SysUserDTO>();
            var limits = _sysRightRepository.GetRightByUserWithModule(request.userId, request.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (limits.IsNotNull() && limits.Count > 0 && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits;
                var query_parameter = new SysUser_Query { PgIndex = 1, PgSize = request.PgSize };
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
