using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ISysErrorLogService
    {
        void Insert(SysErrorLog log);

        Resp_Query<SysErrorLogDTO> Query(SysErrorLog_Query request);


        Resp_SysErrorLog_Index Index(Req_Index request);

    }
}
