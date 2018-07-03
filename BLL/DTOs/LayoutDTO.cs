using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LayoutDTO:BaseEntityDTO
    {
        public string CarNumber { get; set; }

        public string Description { get; set; }

        public string SysUserId { get; set; }

        public string CreateTime { get; set; } = DateTime.Now.ToString();

        /// <summary>
        /// 0：出 1：进 2：进出都触发
        /// </summary>
        public int TriggerType { get; set; }

        public string ValideTime { get; set; }

        public int IsValid { get; set; } = 1;

        /// <summary>
        /// 布控程度 0：代表无限次数 1 代表一次中控，中控完该数据无效
        /// </summary>
        public int Degree { get; set; }

        public string Channel { get; set; }
    }
}
