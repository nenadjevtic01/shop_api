using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; }

    }
}
