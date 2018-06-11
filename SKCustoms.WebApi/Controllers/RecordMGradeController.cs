using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity.Attributes;
using Services;
using Domain;

namespace SKCustoms.WebApi.Controllers
{
    [RoutePrefix("recordmgrade/recordMGrade")]
    public class RecordMGradeController : ApiController
    {
        [Dependency]
        public IRecordMGradeService _recordMGradeService { get; set; }

        [HttpPost]
        [Route("add")]
        public Resp_Binary Add(RecordMGrade model) => _recordMGradeService.Add(model);

        [HttpPost]
        [Route("modify")]
        public Resp_Binary Modify(RecordMGrade model) => _recordMGradeService.Modify(model);

        [HttpPost]
        [Route("del")]
        public Resp_Binary Del(Base_SingleDel request) => _recordMGradeService.Del(request);

        [HttpPost]
        [Route("query")]
        public Resp_Query<RecordMGrade> Query(RMG_Query request) => _recordMGradeService.Query(request);

        [HttpPost]
        [Route("index")]
        public Resp_Index<RecordMGrade> Index(Req_Index request) => _recordMGradeService.Index(request);
    }
}
