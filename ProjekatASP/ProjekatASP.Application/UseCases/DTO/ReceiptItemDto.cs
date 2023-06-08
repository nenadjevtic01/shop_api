using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class ReceiptItemDto
    {
        public int ReceiptItemId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
