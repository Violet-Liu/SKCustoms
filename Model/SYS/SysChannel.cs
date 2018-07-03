using Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SysChannel:BaseEntity<int>
    {
        public SysChannel()
        {
            SysUsers = new HashSet<SysUser>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<SysUser> SysUsers { get; set; }
    }
}
