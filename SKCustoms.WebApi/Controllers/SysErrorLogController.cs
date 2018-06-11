using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity.Attributes;

namespace SKCustoms.WebApi.Controllers
{
    [RoutePrefix("sys/SysErrorLog")]
    public class SysErrorLogController : ApiController
    {
        [Dependency]
        public ISysErrorLogService _sysErrorLogService { get; set; }


        [HttpPost]
        [Route("query")]
        public Resp_Query<SysErrorLogDTO> Query(SysErrorLog_Query request) => _sysErrorLogService.Query(request);

        [HttpPost]
        [Route("index")]
        public Resp_SysErrorLog_Index Index(Req_Index request) => _sysErrorLogService.Index(request);
    }
}
