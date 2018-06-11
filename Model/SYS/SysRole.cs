using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture;

namespace Domain
{
    public class SysRole:BaseEntity<int>
    {
        public SysRole()
        {
            SysRights = new HashSet<SysRight>();
            SysUsers = new HashSet<SysUser>();
        }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public int Creater { get; set; }

        public string Description { get; set; }

        public virtual ICollection<SysRight> SysRights { get; set; }
        public virtual ICollection<SysUser> SysUsers  { get; set; }
    }
}
