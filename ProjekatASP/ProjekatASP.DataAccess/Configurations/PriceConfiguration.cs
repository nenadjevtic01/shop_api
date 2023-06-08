using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class PriceConfiguration : EntityConfiguration<Price>
    {
        public override void ConfigureRules(EntityTypeBuilder<Price> builder)
        {
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.NewPrice).IsRequired().HasPrecision(6, 2);
            builder.Property(x=>x.OldPrice).IsRequired(false).HasPrecision(6, 2);
            builder.Property(x => x.ActiveFrom).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.HasMany(x => x.ReceiptItems).WithOne(x => x.Price).HasForeignKey(x => x.PriceId);
        }
    }
}
