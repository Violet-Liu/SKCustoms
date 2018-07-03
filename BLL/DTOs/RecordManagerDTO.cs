using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RecordManagerDTO:BaseEntityDTO
    {
        public string Type { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 车身颜色
        /// </summary>
        public string CarColor { get; set; }
        /// <summary>
        /// 车的品牌
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// 行驶证
        /// </summary>
        public string TLicense { get; set; }
        /// <summary>
        /// 驾驶证
        /// </summary>
        public string DLicense { get; set; }
        /// <summary>
        /// 驾驶人
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 驾驶人联系方式
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 组织单位
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public string SysUserId { get; set; }

        /// <summary>
        /// 备案时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 最后进场时间
        /// </summary>
        public string LastInTime { get; set; }

        /// <summary>
        /// 最后出场时间
        /// </summary>
        public string LastOutTime { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string ValideTime { get; set; }

        /// <summary>
        /// 分级
        /// </summary>
        public string RecordMGrade { get; set; }

        public int IsValid { get; set; } = 1;

        public string Channel { get; set; }


    }
}
