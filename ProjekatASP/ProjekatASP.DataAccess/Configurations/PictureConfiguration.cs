using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class PictureConfiguration : EntityConfiguration<Picture>
    {
        public override void ConfigureRules(EntityTypeBuilder<Picture> builder)
        {
            builder.Property(x => x.Src).IsRequired().HasMaxLength(128);
            builder.Property(x=>x.Alt).IsRequired().HasMaxLength(128);
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
