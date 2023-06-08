using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Application.UseCases.DTO
{
    public class UserDtoResponse
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }

        public string? Password { get; set; }
        public bool? IsBanned { get; set; }

        public InfoDto? Info { get; set; }

        public CartDto? Cart { get; set; }

        public IEnumerable<UseCaseDto>? UseCases { get; set; }

    }
}
