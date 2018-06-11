using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repostories
{
    public class SysErrorLogConfiguration : EntityTypeConfiguration<SysErrorLog>
    {
        public SysErrorLogConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.ErrIp).HasColumnType("VARCHAR").HasMaxLength(50);
            Property(o => o.ErrMessage).HasColumnType("text");
            Property(o => o.ErrReferrer).HasColumnType("text");
            Property(o=>o.ErrSource).HasColumnType("text");
            Property(o => o.ErrStack).HasColumnType("text");
            Property(o => o.ErrTimestr).HasColumnType("VARCHAR").HasMaxLength(10);
            Property(o => o.ErrType).HasColumnType("VARCHAR").HasMaxLength(200);
            Property(o => o.ErrUrl).HasColumnType("VARCHAR").HasMaxLength(500);
        }
    }
}
