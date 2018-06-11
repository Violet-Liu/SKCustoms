using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services
{
    public partial interface IRecordManagerService
    {
        Resp_Query<RecordManagerDTO> Query(RecordManager_Query request);


        Resp_CheckExsits<RecordManagerDTO> Exsits(Req_CheckExsits request);

        Resp_Binary Add_One(RecordManagerDTO model);

        Resp_Binary Add_Batch(IEnumerable<RecordManagerDTO> models);

        Resp_Binary Del_Batch(IEnumerable<int> ids);

        Resp_Binary Modify(RecordManagerDTO model);

        Resp_Binary Import(File_Import request);

        Resp_Index<RecordManagerDTO> Index(Req_Index request);
    }
}
