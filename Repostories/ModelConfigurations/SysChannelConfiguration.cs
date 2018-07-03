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
    public class SysChannelConfiguration : EntityTypeConfiguration<SysChannel>
    {
        public SysChannelConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.Name).HasColumnType("VARCHAR").IsRequired();
            Property(o => o.Description).HasColumnType("text");
            ToTable("SysChannel");

            this.HasMany(x => x.SysUsers).WithMany(x => x.SysChannels).Map(m => m.ToTable("SysUserChannel").MapLeftKey("SysChannelId").MapRightKey("SysUserId"));
        }
    }
}
