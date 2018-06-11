using Infrastruture;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Alarm : BaseEntity<long>
    {
        public long? CaptureId { get; set; }

        public DateTime AlarmTime { get; set; }

        public string CarNumber { get; set; }

        public int IsDeal { get; set; }

        public string Operator { get; set; }

        public string Message { get; set; }

        public string GUID { get; set; }

        public string LetterCode { get; set; }

        public Nullable<DateTime> HandlerTime { get; set; }

        public virtual Capture Capture { get; set; }

        
    }
}
