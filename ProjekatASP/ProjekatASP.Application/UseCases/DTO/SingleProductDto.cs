using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class SingleProductDto
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public string? Brand { get; set; }
        public string? Gender { get; set; }
        public string? Category { get; set; }
        public bool? Sale { get; set; }
        public bool? InStock { get; set; }
        public string? Material { get; set; }
        public string? CountryOfOrigin { get; set; }

        public decimal? NewPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public IEnumerable<PictureDto>? Pictures {get;set;}
        public IEnumerable<string>? Sizes { get; set; }
    }
}
