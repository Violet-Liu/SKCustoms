using Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SysModuleOperate : BaseEntity<int>
    {
        public SysModuleOperate()
        {
            SysRightOperates = new HashSet<SysRightOperate>();
        }

        public string Name { get; set; }

        public string KeyCode { get; set; }

        public int ModuleId { get; set; }

        public int IsValid { get; set; }

        public int Sort { get; set; }

        public virtual SysModule SysModule { get; set; }

        public virtual ICollection<SysRightOperate> SysRightOperates { get; set; }
    }
}
