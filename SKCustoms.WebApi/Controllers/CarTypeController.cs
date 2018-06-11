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
    [RoutePrefix("recordmanager/CarType")]
    public class CarTypeController : ApiController
    {
        [Dependency]
        public ICarTypeService _carTypeService { get; set; }

        [HttpPost]
        [Route("add")]
        public Resp_Binary Add(CarType model) => _carTypeService.Add(model);

        [HttpPost]
        [Route("modify")]
        public Resp_Binary Modify(CarType model) => _carTypeService.Modify(model);

        [HttpPost]
        [Route("del")]
        public Resp_Binary Del(Base_SingleDel request) => _carTypeService.Del(request);

        [HttpPost]
        [Route("query")]
        public Resp_Query<CarType> Query(RMG_Query request) => _carTypeService.Query(request);

        [HttpPost]
        [Route("index")]
        public Resp_Index<CarType> Index(Req_Index request) => _carTypeService.Index(request);
    }
}
