using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SysModuleOperateDTO
    {
        public int ID { get; set; }
        public int ModuleId { get; set; }

        public string KeyCode { get; set; }

        public string KeyName { get; set; }

        public int Sort { get; set; }

        public int IsValid { get; set; } = 1;

        public int SysRightOperateId { get; set; }

        public int SysRightId { get; set; }

        public int SysModuleOperateId { get; set; }

        public int IsRightValid { get; set; }
    }
}
