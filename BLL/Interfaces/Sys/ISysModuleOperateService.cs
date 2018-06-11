using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ISysModuleOperateService
    {
        Resp_Query<SysModuleOperateDTO> QueryByModuleId(SysModuleOperate_Query request);
    }
}
