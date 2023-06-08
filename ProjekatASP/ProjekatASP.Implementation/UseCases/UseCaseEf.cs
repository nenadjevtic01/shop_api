using ProjekatASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases
{
    public abstract class UseCaseEf
    {
        protected ProjekatAspDbContext Context { get; set; }

        protected UseCaseEf(ProjekatAspDbContext context)
        {
            Context = context;
        }
    }
}
