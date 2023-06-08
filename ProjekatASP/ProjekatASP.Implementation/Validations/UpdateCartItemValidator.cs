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
    public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemDto>
    {
        private readonly ProjekatAspDbContext _context;
        public UpdateCartItemValidator(ProjekatAspDbContext context)
        {
            _context = context;

            RuleFor(x => x.CartItemId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Cart item id is required.")
                .Must(id => _context.CartItems.Any(x => x.Id == id)).WithMessage("Cart item with provided id does not exist.");

            RuleFor(x => x.ProductId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Product id is required.")
                .Must(id => _context.Products.Any(x => x.Id == id)).WithMessage("Product with provided id does not exist.");

            RuleFor(x => x.CartId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Product id is required.")
                .Must(id => _context.Carts.Any(x => x.Id == id)).WithMessage("Cart with provided id does not exist.");

            RuleFor(x => x.SizeId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Size id is required.")
                .Must(id => _context.Sizes.Any(x => x.Id == id)).WithMessage("Size with provided id does not exist");

        }
    }
}
