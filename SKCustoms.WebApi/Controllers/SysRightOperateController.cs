using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using Unity.Attributes;
using Services;
using System.Web.Http.Cors;

namespace SKCustoms.WebApi.Controllers
{
    [RoutePrefix("sys/SysRightOperate")]
    public class SysRightOperateController : ApiController
    {
        [Dependency]
        public ISysRightOperatorService _sysRightOperatorService { get; set; }

        [HttpPost]
        [Route("update_right")]
        public Resp_Binary UpdateRight(SysRightOperate_Update request) => _sysRightOperatorService.UpdateRight(request);

        [HttpPost]
        [Route("get_rightoperates")]
        public Resp_SysModuleOperate GetRightOperates(SysRightOperate_Get request) => _sysRightOperatorService.GetRightOperates(request);

        [HttpPost]
        [Route("index")]
        public Resp_RightOperator_Index Index(Req_Index request) => _sysRightOperatorService.Index(request);

    }
}
