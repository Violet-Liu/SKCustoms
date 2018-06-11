using Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RecordManager:BaseEntity<long>
    {
        /// <summary>
        /// 备案类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 车身颜色
        /// </summary>
        public string CarColor { get; set; }
        /// <summary>
        /// 车的类型
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
        public int SysUserId { get; set; }

        /// <summary>
        /// 备案时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后进场时间
        /// </summary>
        public Nullable<DateTime> LastInTime { get; set; }

        /// <summary>
        /// 最后出场时间
        /// </summary>
        public Nullable<DateTime> LastOutTime { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public Nullable<DateTime> ValideTime { get; set; }

        /// <summary>
        /// 分级
        /// </summary>
        public string RecordMGrade { get; set; }
    }
}
