using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class Price : Entity
    {
        public int ProductId { get; set; }
        public decimal NewPrice { get; set; }
        public decimal? OldPrice { get; set; }

        public DateTime ActiveFrom { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; } = new List<ReceiptItem>();
    }
}
