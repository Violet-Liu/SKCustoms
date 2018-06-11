using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ISysRightService
    {

        Resp_Query<SysRightViewModel> GetRightByUser(SysRightGetByUser request);

        Resp_Query<SysRightViewModel> GetRightByRole(SysRightGetByUser request);

        List<SysModule2DTO> GetRightModuleByUser(int userId);

        Resp_Binary GetAlarmCheckRight(string u_name);
    }
}

