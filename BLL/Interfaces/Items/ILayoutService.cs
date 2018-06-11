using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface ILayoutService
    {
        Resp_Query<LayoutDTO> Query(Layout_Query request);

        Resp_Binary Add_One(LayoutDTO model);

        Resp_Binary Modify(LayoutDTO model);

        Resp_CheckExsits<LayoutDTO> Exsits(Req_CheckExsits request);

        Resp_Binary Add_Batch(IEnumerable<LayoutDTO> models);

        Resp_Binary Valid_Set(Layout_Valid_Set request);

        Resp_Binary_Member<LayoutDTO> Hit(Layout_Hit request);

        Resp_Binary Import(File_Import request);

        Resp_Index<LayoutDTO> Index(Req_Index request);

        Resp_Temporary_Sync Temporary_Sync(Req_Temporary_Sync request);

        Resp_All_Sync All_Sync(Req_All_Sync request);

        Resp_Binary Del(Base_SingleDel model);
    }
}
