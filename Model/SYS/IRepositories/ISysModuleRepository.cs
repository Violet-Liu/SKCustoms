using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture;
using Domain;

namespace Domain
{
    public interface ISysModuleRepository: IRepository<SysModule>
    {
        void InsertSysRight(int ModuleId);

        IEnumerable<SysModule> GetSysModules(string parentId);

        IEnumerable<SysModule> GetSysModulesByUserId(int UserId);
    }
}
