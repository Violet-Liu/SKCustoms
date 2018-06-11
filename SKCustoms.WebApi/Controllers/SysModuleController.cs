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
    [RoutePrefix("sys")]
    public class SysModuleController : ApiController
    {
        [Dependency]
        public ISysModuleService _sysModuleService { get; set; }

        [Dependency]
        public ISysModuleOperateService _sysModuleOperateService { get; set; }


        [Route("SysModule/index")]
        [HttpPost]
        public Resp_Index<SysModuleDTO> Index(Req_Index request) => _sysModuleService.Index(request);

        [Route("SysModule/query")]
        [HttpPost]
        public Resp_Query<SysModuleDTO> Query(SysMoudule_Query request) => _sysModuleService.Query(request);

        [Route("SysModule/create")]
        [HttpPost]
        public Resp_Binary Create(SysModuleDTO model) => _sysModuleService.Create(model);

        [Route("SysModule/del")]
        [HttpPost]
        public Resp_Binary Del(Base_Del_Request request) => _sysModuleService.Del(request);

        [Route("SysModule/modify")]
        [HttpPost]
        public Resp_Binary Modify(SysModuleDTO model) => _sysModuleService.Modify(model);

        [Route("SysModule/add_btn")]
        [HttpPost]
        public Resp_Binary Add_Btn(SysModuleOperateDTO model) => _sysModuleService.Add_Btn(model);

        [Route("SysModule/del_btn")]
        [HttpPost]
        public Resp_Binary Del_Btn(Base_Del_Request request) => _sysModuleService.Del_Btn(request);

        [Route("SysModuleOperate/query_moduleId")]
        public Resp_Query<SysModuleOperateDTO> Query_ModuleId(SysModuleOperate_Query request) => _sysModuleOperateService.QueryByModuleId(request);
    }
}
