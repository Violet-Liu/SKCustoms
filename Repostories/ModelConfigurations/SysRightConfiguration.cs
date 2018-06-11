using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories.ModelConfigurations
{
    public class SysRightConfiguration : EntityTypeConfiguration<SysRight>
    {
        public SysRightConfiguration()
        {
            HasRequired(o => o.SysRole).WithMany(t => t.SysRights).HasForeignKey(p => p.SysRoleId).WillCascadeOnDelete(true);
            HasRequired(o => o.SysModule).WithMany(t => t.SysRights).HasForeignKey(p => p.SysModuleId).WillCascadeOnDelete(true);
            ToTable("SysRight");
        }
    }
}
