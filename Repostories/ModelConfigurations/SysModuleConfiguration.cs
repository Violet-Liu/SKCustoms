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
    public class SysModuleConfiguration : EntityTypeConfiguration<SysModule>
    {
        public SysModuleConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.Name).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            Property(o => o.EnglishName).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o=>o.Url).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.Iconic).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o=>o.Remark).HasColumnType("text");

            HasMany(s => s.SubSysModules).WithOptional(x => x.SuperSysModule).Map(t => t.MapKey("ParentId"));
        }
    }
}
