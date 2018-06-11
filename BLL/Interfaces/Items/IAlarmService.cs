using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial interface IAlarmService
    {
        Resp_Query<AlarmDTO> Query(Alarm_Query request);

        Resp_Binary Check(Alarm_Check request);

        Resp_Binary Add(AlarmDTO model);

        Resp_Index<AlarmDTO> Index(Req_Index request);
    }
}
