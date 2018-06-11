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
    class LayoutConfiguration: EntityTypeConfiguration<Layout>
    {
        public LayoutConfiguration()
        {
            Property(o => o.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.CarNumber).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            HasIndex(o => o.CarNumber);
            Property(o => o.Description).HasColumnType("text");

        }       
    }
}
