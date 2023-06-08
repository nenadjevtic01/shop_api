using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.Exceptions
{
    public class ItemAlreadyExistException : Exception
    {
        public ItemAlreadyExistException(string type):base($"{type} already exist.")
        {

        }
    }
}
