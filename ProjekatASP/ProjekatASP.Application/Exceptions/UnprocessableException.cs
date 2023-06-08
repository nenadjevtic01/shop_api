using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.Exceptions
{
    public class UnprocessableException : Exception
    {
        public UnprocessableException(string name)
            :base($"Can't set duplicate name: " + name)
        {
            
        }
    }
}
