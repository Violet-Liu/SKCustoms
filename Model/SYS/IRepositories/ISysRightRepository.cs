using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture;


namespace Domain
{
    public interface ISysRightRepository : IRepository<SysRight>
    {
        IEnumerable<SysUserRightView> GetRightByUser(int userId);

        IEnumerable<SysUserRightView> GetRightByRole(int roleId);

        IEnumerable<SysUserRightView> GetRightByUserWithModule(int userId, int mouduleId);

        IEnumerable<SysModule> GetRightModuleByUser(int userId);

        IEnumerable<SysUserRightView> GetRightByUserWithModule(int userId, string moduleName, string operateCode);
    }
}
