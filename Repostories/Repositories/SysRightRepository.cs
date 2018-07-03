using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class SysRightRepository : Repository<SysRight>, ISysRightRepository
    {
        public SysRightRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<SysUserRightView> GetRightByRole(int roleId)
        {
            var sql = $@"select  a.ID as RightId,b.Name as moduleName,a.SysModuleId,c.KeyCode,c.Name,b.ParentId,case d.IsValid when 1 then 1 else 0 end as IsValid from SysRight a 
                        inner join SysModule b on a.SysModuleId = b.Id 
                        inner join SysModuleOperate c on c.ModuleId = a.SysModuleId and c.IsValid=1
						left join SysRightOperate d on a.ID=d.RightId and d.SysModuleOperateId=c.ID
                        where a.SysRoleId = {roleId} and c.KeyCode<> '' 
						group by a.ID,a.SysModuleId, b.Name,c.KeyCode,c.Name,b.ParentId,c.Sort,d.IsValid 
                        order by a.SysModuleId,c.Sort";

            return ((IQueryableUnitOfWork)UnitOfWork).ExecuteQuery<SysUserRightView>(sql);
        }

        public IEnumerable<SysUserRightView> GetRightByUser(int userId)
        {
            var sql = $@"select b.Name as moduleName,a.SysModuleId,c.KeyCode,c.Name,b.ParentId,case d.IsValid when 1 then 1 else 0 end as IsValid 
						from SysUser u
						inner join SysUserRole r on r.SysUserId=u.ID 
						inner join SysRight a on  a.SysRoleId=r.SysRoleId 
                        inner join SysModule b on a.SysModuleId = b.Id 
                        inner join SysModuleOperate c on c.ModuleId = a.SysModuleId and c.IsValid=1
						left join SysRightOperate d on a.ID=d.RightId and d.SysModuleOperateId=c.ID
                        where  u.ID={userId} and c.KeyCode<> '' 
						group by a.SysModuleId, b.Name,c.KeyCode,c.Name,b.ParentId,c.Sort,d.IsValid 
                        order by a.SysModuleId,c.Sort;";

            return ((IQueryableUnitOfWork)UnitOfWork).ExecuteQuery<SysUserRightView>(sql);
        }

        public IEnumerable<SysUserRightView> GetRightByUserWithModule(int userId,int mouduleId)
        {
            var sql = $@"select  a.ID as RightId,b.Name as moduleName,a.SysModuleId,c.KeyCode,c.Name,b.ParentId,case d.IsValid when 1 then 1 else 0 end as IsValid 
						from SysUser u
						inner join SysUserRole r on r.SysUserId=u.ID 
						inner join SysRight a on  a.SysRoleId=r.SysRoleId 
                        inner join SysModule b on a.SysModuleId = b.Id 
                        inner join SysModuleOperate c on c.ModuleId = a.SysModuleId and c.IsValid=1
						left join SysRightOperate d on a.ID=d.RightId and d.SysModuleOperateId=c.ID
                        where  u.ID= {userId} and c.KeyCode<> '' and b.ID={mouduleId}
						group by a.ID,a.SysModuleId, b.Name,c.KeyCode,c.Name,b.ParentId,c.Sort,d.IsValid,d.IsValid
                        order by a.SysModuleId,c.Sort";

            return ((IQueryableUnitOfWork)UnitOfWork).ExecuteQuery<SysUserRightView>(sql);
        }

        public IEnumerable<SysModule> GetRightModuleByUser(int userId)
        {
            var sql = $@"select * from SysModule where id in(
						select d.ID from SysUser a
						inner join SysUserRole b on a.ID=b.SysUserId 
						inner join SysRight c on c.=b.SysRoleId and Rightflag=1
						inner join sysModule d on d.id=c.SysModuleId
						where a.ID={userId} group by d.ID)";

            return ((IQueryableUnitOfWork)UnitOfWork).ExecuteQuery<SysModule>(sql);
        }

        public IEnumerable<SysUserRightView> GetRightByUserWithModule(int userId, string moduleName,string operateCode)
        {
            var sql = $@"select  a.ID as RightId,b.Name as moduleName,a.SysModuleId,c.KeyCode,c.Name,b.ParentId,case d.IsValid when 1 then 1 else 0 end as IsValid 
						from SysUser u
						inner join SysUserRole r on r.SysUserId=u.ID 
						inner join SysRight a on  a.SysRoleId=r.SysRoleId 
                        inner join SysModule b on a.SysModuleId = b.Id 
                        inner join SysModuleOperate c on c.ModuleId = a.SysModuleId and c.IsValid=1
						left join SysRightOperate d on a.ID=d.RightId and d.SysModuleOperateId=c.ID
                        where  u.ID= {userId} and c.KeyCode<> '' and b.Name='{moduleName}' and c.keyCode='{operateCode}'
						group by a.ID,a.SysModuleId, b.Name,c.KeyCode,c.Name,b.ParentId,c.Sort,d.IsValid,d.IsValid
                        order by a.SysModuleId,c.Sort";

            return ((IQueryableUnitOfWork)UnitOfWork).ExecuteQuery<SysUserRightView>(sql);
        }

    }
}
