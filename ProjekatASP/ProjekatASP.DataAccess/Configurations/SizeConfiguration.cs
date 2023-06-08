using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class SizeConfiguration : EntityConfiguration<Size>
    {
        public override void ConfigureRules(EntityTypeBuilder<Size> builder)
        {
            builder.Property(x => x.SizeName).IsRequired().HasMaxLength(20);

            builder.HasMany(x => x.ProductSizes).WithOne(x => x.Size).HasForeignKey(x => x.SizeId);
            builder.HasMany(x=>x.CartItems).WithOne(x=>x.Size).HasForeignKey(x => x.SizeId);
            builder.HasMany(x => x.ReceiptItems).WithOne(x => x.Size).HasForeignKey(x => x.SizeId);
        }
    }
}
