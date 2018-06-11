using Model;
using Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class SysUserRepository:Repository<SysUser>,ISysUserRepository
    {
        public SysUserRepository(SKContext context) : base(context)
        {

        }


    }
}
