using FluentValidation;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.Validations
{
    public class CreateCartItemValidator : AbstractValidator<CreateCartItemDto>
    {
        private ProjekatAspDbContext _context;
        public CreateCartItemValidator(ProjekatAspDbContext context)
        {
            _context = context;

            RuleFor(x => x.CartId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Cart id is required.")
                .Must(id =>_context.Carts.Any(x=>x.Id==id)).WithMessage("Cart with provided id does not exist");

            RuleFor(x => x.ProductId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Product id is required.")
                .Must(id => _context.Products.Any(x => x.Id == id)).WithMessage("Product with provided id does not exist");

            RuleFor(x => x.SizeId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Size id is required.")
                .Must(id => _context.Sizes.Any(x => x.Id == id)).WithMessage("Size with provided id does not exist");

            RuleFor(x => x.Quantity).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Quantity is required/at least 1.")
                .GreaterThan(0).WithMessage("Quantity must be at least 1");

        }
    }
}
