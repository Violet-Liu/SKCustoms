using Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Capture:BaseEntity<long>
    {
        public Capture()
        {
            this.Alarms = new HashSet<Alarm>();
        }
        /// <summary>
        /// 停车场id
        /// </summary>
        public string ParkId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 车牌颜色
        /// </summary>
        public string CarColor { get; set; }

        /// <summary>
        /// 车的品牌
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 行政通道
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 进还是出
        /// 0：进 1：出
        /// </summary>
        public int Pass { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否出场
        /// </summary>
        public int WithOut { get; set; }

        public virtual ICollection<Alarm> Alarms { get; set; }

    }
}
