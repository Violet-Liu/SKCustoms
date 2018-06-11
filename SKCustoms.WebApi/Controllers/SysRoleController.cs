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
    [RoutePrefix("sys/SysRole")]
    public class SysRoleController : ApiController
    {
        [Dependency]
        public ISysRoleService _sysRoleService { get; set; }

        [HttpPost]
        [Route("query")]
        public Resp_Query<SysRoleDTO> Query(SysRole_Query request) => _sysRoleService.Query(request);

        [HttpPost]
        [Route("create")]
        public Resp_Binary Create(SysRoleDTO model) => _sysRoleService.Create(model);

        [HttpPost]
        [Route("modify")]
        public Resp_Binary Modify(SysRoleDTO model) => _sysRoleService.Modify(model);


        [HttpPost]
        [Route("assign_users")]
        public Resp_Binary Assign_Users(SysRole_Assign_Users request) => _sysRoleService.Assign_User(request);

        [HttpPost]
        [Route("Del")]
        public Resp_Binary Del(Base_Del_Request request) => _sysRoleService.Del(request);

        [HttpPost]
        [Route("index")]
        public Resp_Index<SysRoleDTO> Index(Req_Index request) => _sysRoleService.Index(request);
    }
}
