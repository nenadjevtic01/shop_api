using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class ReceiptItemConfiguration : EntityConfiguration<ReceiptItem>
    {
        public override void ConfigureRules(EntityTypeBuilder<ReceiptItem> builder)
        {
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.PriceId).IsRequired();
            builder.Property(x => x.SizeId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired().HasMaxLength(3);
            builder.Property(x => x.ReceiptId).IsRequired();
        }
    }
}
