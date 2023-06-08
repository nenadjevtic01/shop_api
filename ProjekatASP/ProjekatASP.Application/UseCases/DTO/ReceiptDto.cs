using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class ReceiptDto
    {
        public int ReceiptId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal Total { get; set; }

        public DateTime OrderedAt { get; set; }

        public IEnumerable<ReceiptItemDto> Items { get; set; }
    }
}
