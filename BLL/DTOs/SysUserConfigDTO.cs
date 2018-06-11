using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SysUserConfigDTO
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }

        public int State { get; set; }


        public int SysUserId { get; set; }
    }
}
