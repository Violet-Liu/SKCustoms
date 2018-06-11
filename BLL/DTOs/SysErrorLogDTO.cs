using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SysErrorLogDTO
    {
        /// <summary>
        /// err_type
        /// </summary>
        public string ErrType { get; set; }

        /// <summary>
        /// err_message
        /// </summary>
        public string ErrMessage { get; set; }

        /// <summary>
        /// err_source
        /// </summary>
        public string ErrSource { get; set; }

        /// <summary>
        /// err_stack
        /// </summary>
        public string ErrStack { get; set; }

        /// <summary>
        /// err_url
        /// </summary>
        public string ErrUrl { get; set; }

        /// <summary>
        /// err_ip
        /// </summary>
        public string ErrIp { get; set; }

        /// <summary>
        /// err_referrer
        /// </summary>
        public string ErrReferrer { get; set; }

        /// <summary>
        /// err_time
        /// </summary>
        public string ErrTime { get; set; }

        public string ErrTimestr { get; set; }
    }
}
