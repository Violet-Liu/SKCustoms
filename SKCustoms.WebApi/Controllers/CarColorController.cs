using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity.Attributes;
using Domain;

namespace SKCustoms.WebApi.Controllers
{
    [RoutePrefix("recordmanager/CarColor")]
    public class CarColorController : ApiController
    {
        [Dependency]
        public ICarColorService _carColorService { get; set; }

        [HttpPost]
        [Route("add")]
        public Resp_Binary Add(CarColor model) => _carColorService.Add(model);

        [HttpPost]
        [Route("modify")]
        public Resp_Binary Modify(CarColor model) => _carColorService.Modify(model);

        [HttpPost]
        [Route("del")]
        public Resp_Binary Del(Base_SingleDel request) => _carColorService.Del(request);

        [HttpPost]
        [Route("query")]
        public Resp_Query<CarColor> Query(RMG_Query request) => _carColorService.Query(request);

        [HttpPost]
        [Route("index")]
        public Resp_Index<CarColor> Index(Req_Index request) => _carColorService.Index(request);
    }
}
