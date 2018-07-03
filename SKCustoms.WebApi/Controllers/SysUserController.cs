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
    [RoutePrefix("sys/SysUser")]
    public class SysUserController : ApiController
    {
        [Dependency]
        public ISysUserService _service { get; set; }

        [HttpPost]
        [Route("query")]
        public Resp_Query<SysUserDTO> Query(SysUser_Query request) => _service.Query(request);

        [HttpPost]
        [Route("create")]
        public Resp_Binary Create(SysUserDTO model) => _service.Create(model);

        [HttpPost]
        [Route("modify")]
        public Resp_Binary Modify(SysUserDTO model) => _service.Modify(model);

        [HttpPost]
        [Route("del")]
        public Resp_Binary Del(Base_Del_Request request) => _service.Del(request);

        [HttpPost]
        [Route("reset_pwd")]
        public Resp_Binary Reset_Pwd(SysUser_Reset_Pwd request) => _service.Reset_Pwd(request);

        [HttpPost]
        [Route("assign_role")]
        public Resp_Binary Assign_Role(SysUser_Assign_Roles request) => _service.Assign_Role(request);

        [HttpPost]
        [Route("assign_channel")]
        public Resp_Binary Assign_Channel(SysUser_Assign_Channels request) => _service.Assign_Channel(request);


        [HttpPost]
        [Route("Index")]
        public Resp_Index<SysUserDTO> Index(Req_Index request) => _service.Index(request);

        [HttpGet]
        [Route("popup")]
        public CaptureDTO Popup(string userId) => _service.Popup(userId);


    }
}
