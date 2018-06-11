using Common;
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
    [RoutePrefix("layout/Layout")]
    public class LayoutController : ApiController
    {
        [Dependency]
        public ILayoutService _service { get; set; }

        [HttpPost]
        [Route("query")]
        public Resp_Query<LayoutDTO> Query(Layout_Query request) => _service.Query(request);

        [HttpPost]
        [Route("add_one")]
        public Resp_Binary Add_One(LayoutDTO model) => _service.Add_One(model);

        [HttpPost]
        [Route("add_batch")]
        public Resp_Binary Add_Batch(List<LayoutDTO> models) => _service.Add_Batch(models);


        [HttpPost]
        [Route("valid_set")]
        public Resp_Binary Valid_Set(Layout_Valid_Set request) => _service.Valid_Set(request);

        [HttpPost]
        [Route("import")]
        public Resp_Binary Import(File_Import request) => _service.Import(request);

        [HttpPost]
        [Route("index")]
        public Resp_Index<LayoutDTO> Index(Req_Index request) => _service.Index(request);

        [HttpPost]
        [Route("temporary_sync")]
        [GZipCompression]
        public Resp_Temporary_Sync Temporary_Sync(Req_Temporary_Sync request) => _service.Temporary_Sync(request);

        [HttpPost]
        [Route("all_sync")]
        [GZipCompression]
        public Resp_All_Sync All_Sync(Req_All_Sync request) => _service.All_Sync(request);


        [HttpPost]
        [Route("hit")]
        public Resp_Binary_Member<LayoutDTO> Hit(Layout_Hit request) => _service.Hit(request);

        [HttpPost]
        [Route("exsits")]
        public Resp_CheckExsits<LayoutDTO> Exsits(Req_CheckExsits request) => _service.Exsits(request);

        [HttpPost]
        [Route("modify")]
        public Resp_Binary Modify(LayoutDTO model) => _service.Modify(model);

        [HttpPost]
        [Route("del")]
        public Resp_Binary Del(Base_SingleDel model) => _service.Del(model);
    }
}
