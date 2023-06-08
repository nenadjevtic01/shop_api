using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class PriceDto
    {
        public decimal Price { get; set; }
        public DateTime ActiveFrom { get; set; }
    }
}
