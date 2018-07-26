using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ICaptureService
    {
        Resp_Query<CaptureDTO> Query(Capture_Query request);

        Resp_Binary_Member<AlarmDTO> Add_One(CaptureDTO model);

        Resp_Index<CaptureDTO> Index(Req_Index request);

        CaptureDTO GetById(int id);

        Resp_Binary Decrease(long id);

    }
}
