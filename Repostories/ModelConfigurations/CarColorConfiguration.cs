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
    public class CarColorConfiguration : EntityTypeConfiguration<CarColor>
    {
        public CarColorConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.Name).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();

        }
    }
}
