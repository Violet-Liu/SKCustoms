using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CaptureDTO: BaseEntityDTO
    { 
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
        /// 0：出 1：进
        /// </summary>
        public string Pass { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        public string StayTime { get; set; }

        public bool IsAlarm { get; set; }

        public string AlarmContent { get; set; }

        public int WithOut { get; set; }
    }
}
