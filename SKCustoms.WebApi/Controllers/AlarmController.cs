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
    [RoutePrefix("alarm/Alarm")]
    public class AlarmController : ApiController
    {
        [Dependency]
        public IAlarmService _service { get; set; }

        [HttpPost]
        [Route("query")]
        public Resp_Query<AlarmDTO> Query(Alarm_Query request) => _service.Query(request);

        [HttpPost]
        [Route("add")]
        public Resp_Binary Add(AlarmDTO model) => _service.Add(model);

        [HttpPost]
        [Route("Check")]
        public Resp_Binary Check(Alarm_Check request) => _service.Check(request);

        [HttpPost]
        [Route("index")]
        public Resp_Index<AlarmDTO> Index(Req_Index request) => _service.Index(request);
    }
}
