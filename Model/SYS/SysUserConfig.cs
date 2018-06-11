using Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SysUserConfig : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }

        public int State { get; set; }


        public int SysUserId { get; set; }
    }
}
