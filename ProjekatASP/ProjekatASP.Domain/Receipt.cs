using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class Receipt : Entity
    {
        public int UserId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal Total { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; }=new List<ReceiptItem>();
    }
}
