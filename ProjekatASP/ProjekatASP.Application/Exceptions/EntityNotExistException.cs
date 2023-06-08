using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.Exceptions
{
    public class EntityNotExistException : Exception
    {
        public EntityNotExistException(string type, int id)
            : base($"Entity type: " + type + " ,Id: " + id + " Status: Not found or not active.")
        {

        }
    }
}
