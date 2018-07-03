using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ISysUserService
    {
        Resp_Binary Create(SysUserDTO model);

        Resp_Query<SysUserDTO> Query(SysUser_Query request);

        Resp_Binary Modify(SysUserDTO model);

        Resp_Binary Del(Base_Del_Request request);

        Resp_Binary Reset_Pwd(SysUser_Reset_Pwd request);

        Resp_Binary Assign_Role(SysUser_Assign_Roles request);

        Resp_Binary Assign_Channel(SysUser_Assign_Channels request);

        Resp_Index<SysUserDTO> Index(Req_Index request);

        CaptureDTO Popup(string userId);

    }
}
