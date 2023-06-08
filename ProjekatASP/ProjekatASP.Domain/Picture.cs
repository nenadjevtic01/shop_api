using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class Picture : Entity
    {
        public string Src { get; set; }
        public string Alt { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
