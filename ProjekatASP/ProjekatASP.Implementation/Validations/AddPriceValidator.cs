using FluentValidation;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.Validations
{
    public class AddPriceValidator : AbstractValidator<CreatePriceDto>
    {
        private ProjekatAspDbContext _context;
        public AddPriceValidator(ProjekatAspDbContext context)
        {

            _context = context;

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required.");

            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product id is required.")
                .Must(id =>!_context.Products.Any(x=>x.Id==id)).WithMessage("Product with provided id does not exist.");

            RuleFor(x => x.ValidFrom).NotEmpty().WithMessage("Valid from date is required")
                .Must(date => date >= DateTime.UtcNow.AddMonths(-24)).WithMessage("Valid from date must be valid maximum from 2 years ago.");
        }
    }
}
