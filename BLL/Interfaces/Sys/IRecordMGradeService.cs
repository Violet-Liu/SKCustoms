using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRecordMGradeService
    {
        Resp_Binary Add(RecordMGrade model);

        Resp_Binary Modify(RecordMGrade model);

        Resp_Binary Del(Base_SingleDel model);

        Resp_Query<RecordMGrade> Query(RMG_Query request);

        Resp_Index<RecordMGrade> Index(Req_Index request);
    }
}
