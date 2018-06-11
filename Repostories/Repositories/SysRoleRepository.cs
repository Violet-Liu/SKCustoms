using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public partial class SysRoleRepository : Repository<SysRole>, ISysRoleRepository
    {
        public SysRoleRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}
