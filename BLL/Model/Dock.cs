using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Req_Warning
    {
        public string WARNINGDATE { get; set; }

        public string CARNO { get; set; }

        public string CARTYPE { get; set; }

        public string WARNINGTYPE { get; set; }

        public string inout { get; set; }

        public string bayonet { get; set; }

        public string Remark { get; set; }
    }

    public class Resp_Warning
    {
        public bool Success { get; set; }

        public int InsertNum { get; set; }

        public string msg { get; set; }
    }


    public enum WarningType
    {
        车辆识别中控,
        车型识别异常
    }

    public enum InOut
    {
        进,
        出
    }
}
