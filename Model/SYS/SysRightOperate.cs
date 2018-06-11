using Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SysRightOperate : BaseEntity<int>
    {
        public int RightId { get; set; }

        public int SysModuleOperateId { get; set; }

        public int IsValid { get; set; }

        public virtual SysRight SysRight { get; set; }

        public virtual SysModuleOperate SysModuleOperate { get; set; }

    }
}
