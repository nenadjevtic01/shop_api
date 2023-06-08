using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class Info : Entity
    {
        public string Address { get;set; }
        public string City { get;set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
