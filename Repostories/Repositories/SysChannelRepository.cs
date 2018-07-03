using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repostories.Repositories
{
    public class SysChannelRepository : Repository<SysChannel>, ISysChannelRepository
    {
        public SysChannelRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}
