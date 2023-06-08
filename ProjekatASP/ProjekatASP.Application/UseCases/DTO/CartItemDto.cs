using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class CartItemDto
    {
        public int? CartItemId { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }

    }
}
