using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class ReceiptConfiguration : EntityConfiguration<Receipt>
    {
        public override void ConfigureRules(EntityTypeBuilder<Receipt> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Subtotal).IsRequired().HasPrecision(7, 2);
            builder.Property(x => x.ShippingFee).IsRequired().HasPrecision(6, 2);
            builder.Property(x => x.Total).IsRequired().HasPrecision(7, 2);

            builder.HasMany(x => x.ReceiptItems).WithOne(x => x.Receipt).HasForeignKey(x => x.ReceiptId);
        }
    }
}
