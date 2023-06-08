using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.Exceptions
{
    public class ForbiddenQueryException : Exception
    {
        public ForbiddenQueryException(string user):base("User has tried to see someone else receipt.")
        {

        }
    }
}
