using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories.ModelConfigurations
{
    public class SysRightOperateConfiguration : EntityTypeConfiguration<SysRightOperate>
    {
        public SysRightOperateConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(r => r.SysRight).WithMany(t => t.SysRightOperates).HasForeignKey(f => f.RightId).WillCascadeOnDelete(true);
            HasRequired(r => r.SysModuleOperate).WithMany(t => t.SysRightOperates).HasForeignKey(f => f.SysModuleOperateId).WillCascadeOnDelete(true);
        }
    }
}
