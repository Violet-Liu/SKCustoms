using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastruture;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class SysModule:BaseEntity<int>
    {
        public SysModule()
        {
            this.SubSysModules = new HashSet<SysModule>();
            this.SysModuleOperates = new HashSet<SysModuleOperate>();
            this.SysRights = new HashSet<SysRight>();
        }

        public string Name { get; set; }

        public string EnglishName { get; set; }

        public string Url { get; set; }

        public string Iconic { get; set; }

        public Nullable<int> Sort { get; set; }

        public string Remark { get; set; }

        public int Enable { get; set; }

        public DateTime CreateTime { get; set; }

        public int Creater { get; set; }

        public int IsLast { get; set; }

        public virtual ICollection<SysModule> SubSysModules { get; set; }

        public virtual ICollection<SysRight> SysRights { get; set; }

        public virtual SysModule SuperSysModule { get; set; }

        public virtual ICollection<SysModuleOperate> SysModuleOperates { get; set; }
    }
}
