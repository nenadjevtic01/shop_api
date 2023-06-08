using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class InfoConfiguration : EntityConfiguration<Info>
    {
        public override void ConfigureRules(EntityTypeBuilder<Info> builder)
        {
            builder.Property(x => x.Address).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.City).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.PostalCode).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Country).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UserId).IsRequired();

            builder.HasIndex(x => x.PostalCode);
            builder.HasIndex(x => x.Country);
        }
    }
}
