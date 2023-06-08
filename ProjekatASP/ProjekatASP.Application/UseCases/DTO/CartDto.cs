using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class CartDto
    {
        public int? CartId { get; set; }
        public IEnumerable<CartItemDto>? Items { get; set; }
    }
}
