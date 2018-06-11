using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class SysRightOperateRepository : Repository<SysRightOperate>, ISysRightOperateRepository
    {
        public SysRightOperateRepository(SKContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}
