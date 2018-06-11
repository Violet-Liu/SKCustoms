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
    public class SysUserConfiguration : EntityTypeConfiguration<SysUser>
    {
        public SysUserConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.Name).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            Property(o => o.Pwd).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            Property(o => o.Sex).HasColumnType("VARCHAR").HasMaxLength(10).IsRequired();
            Property(o => o.Position).HasColumnType("VARCHAR").HasMaxLength(100);
            Property(o => o.Remark).HasColumnType("text");
            Property(o => o.Contact).HasColumnType("VARCHAR").HasMaxLength(50);
            Property(o => o.CreateTime).IsRequired();
            Property(o => o.Creater).IsRequired();
            ToTable("SysUser");

        }

        
    }
}
