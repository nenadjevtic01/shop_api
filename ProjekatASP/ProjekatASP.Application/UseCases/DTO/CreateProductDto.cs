using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class CreateProductDto
    {
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int GenderId { get; set; }
        public bool Sale { get; set; }
        public bool InStock { get; set; }
        public string? Material { get; set; }
        public string? CountryOfOrigin { get; set; }
        public PriceDto? Price { get; set; }

        public IEnumerable<PictureDto>? Pictures { get; set; }
        public IEnumerable<int>? Sizes { get; set; }
    }
}
