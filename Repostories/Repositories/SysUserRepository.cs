using Domain;
using Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class SysUserRepository:Repository<SysUser>,ISysUserRepository
    {
        public SysUserRepository(SKContext context) : base(context)
        {

        }

    }
}
