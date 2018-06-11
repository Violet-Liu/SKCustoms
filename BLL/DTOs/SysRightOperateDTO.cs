using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SysRightOperateDTO
    {
        public int Id { get; set; }
        public int RightId { get; set; }

        public int SysModuleOperateId { get; set; }

        public int IsValid { get; set; }
    }
}
