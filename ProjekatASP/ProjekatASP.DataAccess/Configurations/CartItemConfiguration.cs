using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class CartItemConfiguration : EntityConfiguration<CartItem>
    {
        public override void ConfigureRules(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(x => x.CartId).IsRequired();
            builder.Property(x=>x.ProductId).IsRequired();
            builder.Property(x=>x.Quantity).IsRequired();
            builder.Property(x=>x.SizeId).IsRequired();
            
        }
    }
}
