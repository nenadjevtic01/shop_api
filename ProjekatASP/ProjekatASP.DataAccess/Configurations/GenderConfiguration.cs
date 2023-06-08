using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class GenderConfiguration : EntityConfiguration<Gender>
    {
        public override void ConfigureRules(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(x => x.GenderName).IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.Products).WithOne(x => x.Gender).HasForeignKey(x => x.GenderId);
        }
    }
}
