using Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity.Attributes;

namespace SKCustoms.WebApi.Controllers
{
    [RoutePrefix("sys/SysChannel")]
    public class SysChannelController : ApiController
    {
        [Dependency]
        public ISysChannelService sysChannelService { get; set; }

        [HttpPost]
        [Route("query")]
        public Resp_Query<SysChannelDTO> Query(SysRole_Query request) => sysChannelService.Query(request);

        [HttpPost]
        [Route("create")]
        public Resp_Binary Create(SysChannel model) => sysChannelService.Create(model);

        [HttpPost]
        [Route("modify")]
        public Resp_Binary Modify(SysChannel model) => sysChannelService.Modify(model);


        [HttpPost]
        [Route("assign_users")]
        public Resp_Binary Assign_Users(SysChannel_Assign_Users request) => sysChannelService.Assign_User(request);

        [HttpPost]
        [Route("Del")]
        public Resp_Binary Del(Base_SingleDel request) => sysChannelService.Del(request);

        [HttpPost]
        [Route("index")]
        public Resp_Index<SysChannelDTO> Index(Req_Index request) => sysChannelService.Index(request);

    }
}
