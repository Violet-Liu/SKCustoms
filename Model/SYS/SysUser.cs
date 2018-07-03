using Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SysUser: BaseEntity<int>
    {
        public SysUser()
        {
            SysRoles = new HashSet<SysRole>();
            SysChannels = new HashSet<SysChannel>();
        }
        public string Pwd { get; set; }

        public string Name { get; set; }

        public string TrueName { get; set; }

        public string Sex { get; set; } 

        public DateTime CreateTime { get; set; }

        public int Creater { get; set; }

        public string Position { get; set; }

        public string Remark { get; set; }

        public string Contact { get; set; }

        public int State { get; set; }

        public virtual ICollection<SysRole> SysRoles { get; set; }

        public virtual ICollection<SysChannel> SysChannels { get; set; }
    }
}
