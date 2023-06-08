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
    public class UserConfiguration : EntityConfiguration<User>
    {
        public override void ConfigureRules(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.LastName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Username).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Password).IsRequired().HasMaxLength(100);
            builder.Property(x => x.IsBanned).IsRequired();
            builder.Property(x => x.Role).IsRequired().HasDefaultValue(2);

            builder.HasMany(x => x.UserUseCases).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Receipts).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Info).WithOne(x => x.User).HasForeignKey<Info>(x => x.UserId);
            builder.HasOne(x => x.Cart).WithOne(x => x.User).HasForeignKey<Cart>(x => x.UserId);
        }
    }
}
