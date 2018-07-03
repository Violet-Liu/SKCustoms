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
    public class AlarmConfiguration : EntityTypeConfiguration<Alarm>
    {
        public AlarmConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.CarNumber).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            HasIndex(o => o.CarNumber);
            Property(o => o.Operator).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.LetterCode).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.Channel).HasColumnType("VARCHAR").HasMaxLength(200);
            HasIndex(o => o.Channel);
            HasOptional(o => o.Capture).WithMany(s=>s.Alarms).HasForeignKey(o=>o.CaptureId).WillCascadeOnDelete(true);

        }
    }
}
