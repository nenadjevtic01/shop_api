using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.Validations
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        private ProjekatAspDbContext _context;
        public CreateProductValidator(ProjekatAspDbContext context)
        {
            _context = context;

            RuleFor(x=>x.ProductName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(5).WithMessage("Minimum product name length is 5 characters.")
                .MaximumLength(25).WithMessage("Maximum product name length is 25 characters.")
                .Matches(@"^[A-Za-z0-9 ]+$").WithMessage("Product must have only letters and numbers.")
                .Must(name => !_context.Products.Any(x => x.ProductName == name)).WithMessage("Product with provided name already exist."); ;

            RuleFor(x=>x.CategoryId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Category id is required.")
                .Must(id=> _context.Categories.Any(x=>x.Id== id)).WithMessage("Category with provided id does not exist.");

            RuleFor(x => x.BrandId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Brand id is required.")
                .Must(id => _context.Brands.Any(x => x.Id == id)).WithMessage("Brand with provided id does not exist.");

            RuleFor(x=>x.GenderId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Gender id is required.")
                .Must(id => _context.Genders.Any(x => x.Id == id)).WithMessage("Gender with provided id does not exist.");

            RuleFor(x => x.Sale).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Sale status is required.");

            RuleFor(x => x.InStock).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("In stock status is required.");

            RuleFor(x => x.Material).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Material is required.");

            RuleFor(x => x.CountryOfOrigin).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Country of origin is required.");

            RuleFor(x => x.Price).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Price is required")
                .ChildRules(price =>
                {
                    price.RuleFor(x => x.ActiveFrom).Cascade(CascadeMode.Stop)
                        .NotEmpty().WithMessage("Date from which the price is active is required.");

                    price.RuleFor(x => x.Price).Cascade(CascadeMode.Stop)
                        .NotEmpty().WithMessage("Price is required.");
                });

            RuleFor(x => x.Sizes).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Minimum number of sizes is 1.")
                .Must(size =>
                {
                    if (size == null)
                    {
                        return true;
                    }

                    return size.Distinct().Count() == size.Count();
                }).WithMessage("Duplicate size id are not allowed.").DependentRules(() =>
                {
                    RuleForEach(x => x.Sizes).NotEmpty().WithMessage("Size id not be empty.");
                    RuleForEach(x => x.Sizes).Must(x => _context.Sizes.Any(y => y.Id == x)).WithMessage("Size with provided id does not exist");
                });

            RuleFor(x => x.Pictures).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Pictures are required")
                .Must(isUnique).WithMessage("Duplicate picture src are not allowed.");

            RuleForEach(x => x.Pictures).SetValidator(new PictureDtoValidator());
        }

        public bool isUnique(IEnumerable<PictureDto> pictures)
        {
            var srcs = new HashSet<string>();
            foreach (var picture in pictures)
            {
                if (srcs.Contains(picture.Src))
                    return false;
                srcs.Add(picture.Src);
            }
            return true;
        }
    }
}
