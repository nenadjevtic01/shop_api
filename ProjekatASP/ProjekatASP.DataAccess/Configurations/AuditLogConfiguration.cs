using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Configurations
{
    public class AuditLogConfiguration : EntityConfiguration<AuditLog>
    {
        public override void ConfigureRules(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
