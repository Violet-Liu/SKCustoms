using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AlarmDTO:BaseEntityDTO
    {
        public string CaptureId { get; set; }

        public string AlarmTime { get; set; }

        public string CarNumber { get; set; }

        public int IsDeal { get; set; }

        public string Operator { get; set; }

        public string Message { get; set; }

        public string HandlerTime { get; set; }

        public string GUID { get; set; }

        public string LetterCode { get; set; }

        public string Channel { get; set; }
    }
}
