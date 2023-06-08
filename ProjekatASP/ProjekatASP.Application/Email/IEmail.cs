using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.Email
{
    public interface IEmail
    {
        void Send(MessageDto message);
    }
}
