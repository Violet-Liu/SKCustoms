using Common;
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
    [RoutePrefix("capture/Capture")]
    public class CaptureController : ApiController
    {
        [Dependency]
        public ICaptureService _service { get; set; }

        [Route("query")]
        [HttpPost]
        public Resp_Query<CaptureDTO> Query(Capture_Query request) => _service.Query(request);

        [Route("add_one")]
        [HttpPost]
        public Resp_Binary_Member<AlarmDTO> Add_One(CaptureDTO model) => _service.Add_One(model);

        [Route("index")]
        [HttpPost]
        public Resp_Index<CaptureDTO> Index(Req_Index request) => _service.Index(request);
    }
}
