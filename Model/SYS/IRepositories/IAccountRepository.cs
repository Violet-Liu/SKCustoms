using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture;

namespace Domain
{
    public interface IAccountRepository
    {
        SysUser Login(string name, string pwd);
    }
}
