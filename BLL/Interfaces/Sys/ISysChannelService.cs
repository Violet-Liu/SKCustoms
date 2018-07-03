using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISysChannelService
    {
        Resp_Index<SysChannelDTO> Index(Req_Index index);

        Resp_Query<SysChannelDTO> Query(SysRole_Query request);

        Resp_Binary Create(SysChannel model);

        Resp_Binary Modify(SysChannel model);

        Resp_Binary Del(Base_SingleDel request);

        Resp_Binary Assign_User(SysChannel_Assign_Users request);

    }
}
