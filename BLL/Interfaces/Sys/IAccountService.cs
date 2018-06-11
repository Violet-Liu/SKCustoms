using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface IAccountService
    {
        SysUserDTO Login(string name, string pwd);

        Resp_Binary ResetPwd(Req_ResetPwd request);
    }
}
