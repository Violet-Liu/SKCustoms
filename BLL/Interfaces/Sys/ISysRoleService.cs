using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ISysRoleService
    {
        Resp_Query<SysRoleDTO> Query(SysRole_Query request);

        Resp_Binary Create(SysRoleDTO model);

        Resp_Binary Modify(SysRoleDTO model);

        Resp_Binary Del(Base_Del_Request request);

        Resp_Binary Assign_User(SysRole_Assign_Users request);

        Resp_Index<SysRoleDTO> Index(Req_Index request);
    }
}
