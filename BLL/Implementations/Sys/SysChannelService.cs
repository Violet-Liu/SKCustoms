using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain;
using Infrastructure;
using Repostories;
using Services.Mapping;
using Unity.Attributes;

namespace Services
{
    public class SysChannelService : ISysChannelService
    {
        [Dependency]
        public ISysChannelRepository sysChannelRepository { get; set; }

        [Dependency]
        public ISysRightRepository _sysRightRepository { get; set; }


        public Resp_Binary Assign_User(SysChannel_Assign_Users request)
        {

            using (var context = new SKContext())
            {
                var sysChannel = context.SysChannels.Where(t => t.ID == request.ChannelId).FirstOrDefault();
                if (sysChannel.IsNull())
                    return new Resp_Binary { message = "行政通道不存在" };
                sysChannel.SysUsers.Clear();
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

                    sysChannel.SysUsers = sysUsers;
                }
                if (context.SaveChanges() > 0)
                    return Resp_Binary.Modify_Sucess;
            }
            return Resp_Binary.Modify_Failed;
        }

        public Resp_Binary Create(SysChannel model)
        {
            if (sysChannelRepository.GetByWhere(t => t.Name == model.Name).FirstOrDefault().IsNotNull())
                return new Resp_Binary { message = "行政通道名已存在" };
            sysChannelRepository.Insert(model);
            if (sysChannelRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Add_Sucess;

            return Resp_Binary.Add_Failed;
        }

        public Resp_Binary Del(Base_SingleDel request)
        {
            if (request.Id>0)
            {
                sysChannelRepository.DeleteById(request.Id);
                if (sysChannelRepository.UnitOfWork.Commite() > 0)
                    return Resp_Binary.Del_Sucess;
            }
            else
            {
                return new Resp_Binary { message = "请选择要操作的记录" };
            }

            return Resp_Binary.Del_Failed;
        }

        public Resp_Index<SysChannelDTO> Index(Req_Index index)
        {
            var response = new Resp_Index<SysChannelDTO>();
            var limits = _sysRightRepository.GetRightByUserWithModule(index.userId, index.moduleId).ConvertoDto<SysUserRightView, SysModuleOperateIndexDTO>().ToList();
            if (limits.IsNotNull() && limits.Count > 0 && limits.Find(s => s.IsValid == 1).IsNotNull())
            {
                response.allowVisit = true;
                response.moduleOperaties = limits.OrderByDescending(t => t.IsValid).GroupBy(t => new { t.KeyCode, t.KeyName }).Select(s => new SysModuleOperateIndexDTO
                {
                    KeyCode = s.Key.KeyCode,
                    KeyName = s.Key.KeyName,
                    IsValid = s.Sum(x => x.IsValid),
                }).ToList();
                var query_parameter = new SysRole_Query { PgIndex = 1, PgSize = index.PgSize };
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

        public Resp_Binary Modify(SysChannel model)
        {
            var entity = sysChannelRepository.GetById(model.ID);
            if (entity == null)
                return new Resp_Binary { message = "行政通道不存在" };

            entity.Name = model.Name;
            entity.Description = model.Description;

            sysChannelRepository.Update(entity);
            if (sysChannelRepository.UnitOfWork.Commite() > 0)
                return Resp_Binary.Modify_Sucess;

            return Resp_Binary.Modify_Failed;
        }

        public Resp_Query<SysChannelDTO> Query(SysRole_Query request)
        {
            var response = new Resp_Query<SysChannelDTO>();

            var records = sysChannelRepository.GetAll();

            records.ToMaybe()
                .Do(d => request.Verify())  //系统角色不展现
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
            response.entities = entities.ConvertoDto<SysChannel, SysChannelDTO>().ToList();
            response.entities.ForEach(f =>
            {
                var temp = entities.Find(t => t.ID == f.ID);
                var UserName = new StringBuilder();
                if (temp.IsNotNull())
                {
                    temp.SysUsers.ToList().ForEach(s => UserName.Append("[" + s.Name + "]"));
                }
                f.UserName = UserName.ToString();
            });
            return response;
        }
    }
}
