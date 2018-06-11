using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;
using Services.Mapping;

namespace Services
{
    public class SysModuleOperateService : ISysModuleOperateService
    {

        [Dependency]
        public ISysModuleOperateRepository _sysModuleOperateRepository { get; set; }

        public Resp_Query<SysModuleOperateDTO> QueryByModuleId(SysModuleOperate_Query request)
        {
            var response = new Resp_Query<SysModuleOperateDTO>();
            response.entities = _sysModuleOperateRepository.GetByWhere(t => t.ModuleId == request.ModuleId).ConvertoDto<SysModuleOperate, SysModuleOperateDTO>().ToList();
            response.totalCounts = response.entities.Count;
            response.totalRows = 1;

            return response;
        }
    }
}
