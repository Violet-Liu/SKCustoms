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

    public class CaptureConfiguration : EntityTypeConfiguration<Capture>
    {
        public CaptureConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.ParkId).HasColumnType("VARCHAR").HasMaxLength(50);
            Property(o => o.CarNumber).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            HasIndex(o => o.CarNumber);
            Property(o => o.CarColor).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.CarType).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.Channel).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.ImageUrl).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.Remark).HasColumnType("text");
        }
    }
}
