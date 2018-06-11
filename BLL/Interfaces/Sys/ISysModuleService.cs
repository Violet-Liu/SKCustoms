using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ISysModuleService
    {
        Resp_Query<SysModuleDTO> Query(SysMoudule_Query request);

        Resp_Binary Create(SysModuleDTO model);

        Resp_Binary Del(Base_Del_Request request);

        Resp_Binary Modify(SysModuleDTO model);

        Resp_Binary Add_Btn(SysModuleOperateDTO request);

        Resp_Binary Del_Btn(Base_Del_Request request);

        Resp_Index<SysModuleDTO> Index(Req_Index request);
    }
}
