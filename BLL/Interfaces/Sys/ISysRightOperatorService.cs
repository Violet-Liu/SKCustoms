using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ISysRightOperatorService
    {

        Resp_Binary UpdateRight(SysRightOperate_Update request);

        Resp_SysModuleOperate GetRightOperates(SysRightOperate_Get request);

        Resp_RightOperator_Index Index(Req_Index request);
    }
}
