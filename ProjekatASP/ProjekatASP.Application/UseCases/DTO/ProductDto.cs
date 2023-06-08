using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
