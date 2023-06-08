using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class CartConfiguration : EntityConfiguration<Cart>
    {
        public override void ConfigureRules(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(x => x.UserId).IsRequired();

            builder.HasMany(x => x.Items).WithOne(x => x.Cart).HasForeignKey(x => x.CartId);
        }
    }
}
