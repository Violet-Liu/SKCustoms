using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class SysModuleRepository : Repository<SysModule>, ISysModuleRepository
    {
        public SysModuleRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }

        

        public IEnumerable<SysModule> GetSysModulesByUserId(int UserId)
        {
            var sql = $@"select m.* from sysmodule m
                            inner join sysright r on r.SysModuleId = m.ID and r.Rightflag = 1
                            inner join sysuserrole ur on ur.SysRoleId = r.SysRoleId and ur.SysUserId = {UserId}";

            return ((IQueryableUnitOfWork)UnitOfWork).ExecuteQuery<SysModule>(sql);
        }

        public IEnumerable<SysModule> GetSysModules(string parentId)
        {
            var sql = "SELECT ID,Name,EnglishName,Url,Iconic,Sort,Remark,Enable,CreateTime,Creater,IsLast from sysModule";
            if (int.TryParse(parentId, out int result))
                sql = sql +$" where ParentId={result} order by sort";
            else
                sql = sql + " where ISNULL(ParentId) || LENGTH(trim(ParentId))<1 order by sort ";

            return ((IQueryableUnitOfWork)UnitOfWork).ExecuteQuery<SysModule>(sql);
        }

        public void InsertSysRight(int ModuleId)
        {
            var sql =
            ((IQueryableUnitOfWork)UnitOfWork).ExecuteCommand($"insert into SysRight(SysModuleId,SysRoleId,RightFlag)select {ModuleId}, id, 0 from SysRole");
        }
    }
}
