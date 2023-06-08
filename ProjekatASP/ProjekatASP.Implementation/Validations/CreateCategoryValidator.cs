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
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        private ProjekatAspDbContext _context;

        public CreateCategoryValidator(ProjekatAspDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Minimum name length is 3 characters")
                .MaximumLength(25)
                .WithMessage("Maximum name length is 25 characters")
                .Must(CategoryNotInUse)
                .WithMessage("Category name already exists.")
                .Matches(@"^[A-Za-z ]+$").WithMessage("Category must have only letters.");

            _context = context;
        }
        private bool CategoryNotInUse(string name)
        {
            return !_context.Categories.Any(x=>x.CategoryName==name);
        }
    }
}
