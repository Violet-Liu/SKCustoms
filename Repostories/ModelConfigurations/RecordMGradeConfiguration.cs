using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public class RecordMGradeConfiguration : EntityTypeConfiguration<RecordMGrade>
    {
        public RecordMGradeConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.Name).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            Property(o => o.Description).HasColumnType("text");
        }
    }
}
