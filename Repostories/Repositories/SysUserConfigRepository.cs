using Common;
using Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Domain;

namespace Repostories
{
    public class SysUserConfigRepository : Repository<SysUserConfig>, ISysUserConfigRepository
    {
        public SysUserConfigRepository(IQueryableUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
