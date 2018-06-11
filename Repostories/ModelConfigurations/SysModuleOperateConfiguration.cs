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
    public class SysModuleOperateConfiguration : EntityTypeConfiguration<SysModuleOperate>
    {
        public SysModuleOperateConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.Name).HasColumnType("VARCHAR").HasMaxLength(200);

            HasRequired(t => t.SysModule).WithMany(t => t.SysModuleOperates).HasForeignKey(f => f.ModuleId).WillCascadeOnDelete(true);//级联删除
        }
    }
}
