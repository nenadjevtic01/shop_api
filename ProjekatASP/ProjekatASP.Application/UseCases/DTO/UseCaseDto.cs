using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class UseCaseDto : BaseDto
    {
        public string? UseCaseName { get; set; }
        public string? User { get; set; }
    }
}
