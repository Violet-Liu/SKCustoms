using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class SysErrorLogRepository : Repository<SysErrorLog>, ISysErrorLogRepository
    {
        public SysErrorLogRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}
