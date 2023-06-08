using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class BrandConfiguration : EntityConfiguration<Brand>
    {
        public override void ConfigureRules(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.BrandName).IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.Products).WithOne(x => x.Brand).HasForeignKey(x => x.BrandId);
        }
    }
}
