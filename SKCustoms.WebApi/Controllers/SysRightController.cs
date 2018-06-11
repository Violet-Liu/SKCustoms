using Domain;
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
    [RoutePrefix("sys/SysRight")]
    public class SysRightController : ApiController
    {
        [Dependency]
        public ISysRightService _sysRightService { get; set; }

        [Route("getrightbyuser")]
        [HttpPost]
        public Resp_Query<SysRightViewModel> GetRightByUser(SysRightGetByUser request) => _sysRightService.GetRightByUser(request);

        [Route("getrightbyrole")]
        [HttpPost]
        public Resp_Query<SysRightViewModel> GetRightByRole(SysRightGetByUser request) => _sysRightService.GetRightByRole(request);


        [Route("getcheckrightbyuser")]
        [HttpPost]
        public Resp_Binary GetCheckRightByUser(Login request) => _sysRightService.GetAlarmCheckRight(request.userName);

    }
}
