using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class Cart : Entity
    {
        public int UserId { get; set; }

        public virtual User  User { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }=new List<CartItem>();
    }
}
