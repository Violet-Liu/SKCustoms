using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture;

namespace Domain
{
    public class SysRight:BaseEntity<int>
    {
        public SysRight()
        {
            SysRightOperates = new HashSet<SysRightOperate>();
        }

        public int SysModuleId { get; set; }

        public int SysRoleId { get; set; }

        public int Rightflag { get; set; }


        public virtual SysModule SysModule { get; set; }
        public virtual SysRole SysRole { get; set; }

        public virtual ICollection<SysRightOperate> SysRightOperates { get; set; }

    }
}
