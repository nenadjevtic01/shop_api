using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class Brand : Entity
    {
        public string BrandName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
