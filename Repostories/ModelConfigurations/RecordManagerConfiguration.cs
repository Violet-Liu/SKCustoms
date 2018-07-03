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
    public class RecordManagerConfiguration : EntityTypeConfiguration<RecordManager>
    {
        public RecordManagerConfiguration()
        {
            Property(o=>o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.CarNumber).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            HasIndex(o => o.CarNumber);
            Property(o => o.CarColor).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.CarType).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.TLicense).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.DLicense).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.Driver).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.Contact).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.Organization).HasColumnType("VARCHAR").HasMaxLength(500);
            Property(o => o.RecordMGrade).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.Channel).HasColumnType("VARCHAR").HasMaxLength(200);
            HasIndex(o => o.Channel);
        }
    }
}
