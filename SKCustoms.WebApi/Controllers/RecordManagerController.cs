using Core;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity.Attributes;
using Common;
using Domain;
using System.Web.Http.Cors;

namespace SKCustoms.WebApi.Controllers
{
    [RoutePrefix("recordmanager/RecordManager")]
    public class RecordManagerController : ApiController
    {
        [Dependency]
        public IRecordManagerService _service { get; set; }


        [Route("query")]
        [HttpPost]
        public Resp_Query<RecordManagerDTO> Query(RecordManager_Query req) => _service.Query(req);

        [Route("add")]
        [HttpPost]
        public Resp_Binary Add(Req_RecordManager_Add request) => _service.Add(request);

        [Route("add_one")]
        [HttpPost]
        public Resp_Binary Add_One(RecordManagerDTO manager) => _service.Add_One(manager);

        [Route("exsits")]
        [HttpPost]
        public Resp_CheckExsits<RecordManagerDTO> Exsits(Req_CheckExsits request) => _service.Exsits(request);

        [Route("add_batch")]
        [HttpPost]
        public Resp_Binary Add_Batch(List<RecordManagerDTO> managers) => _service.Add_Batch(managers);

        [Route("del_batch")]
        [HttpPost]
        public Resp_Binary Del_Batch(Base_Del_Request req) => _service.Del_Batch(req.Ids);

        [Route("modify")]
        [HttpPost]
        public Resp_Binary Modify(RecordManagerDTO manager) => _service.Modify(manager);

        [Route("import")]
        [HttpPost]
        public Resp_Binary Import(File_Import request) => _service.Import(request);

        [Route("index")]
        [HttpPost]
        public Resp_Index<RecordManagerDTO> Index(Req_Index request) => _service.Index(request);
    }
}
