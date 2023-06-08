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
    public class UpdateUserInfoValidator : AbstractValidator<UpdateUserInfoDto>
    {
        private readonly ProjekatAspDbContext _context;
        public UpdateUserInfoValidator(ProjekatAspDbContext context)
        {
            _context = context;

            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("User id is required.")
                .Must(user=> _context.Users.Any(x=>x.Id==user)).WithMessage("User with provided id does not exist.");

            RuleFor(x => x.Address).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Address is required.")
                    .When(x => x.Address != null && x.Address.Any())
                    .Matches(@"^[a-zA-Z0-9 ]{6,20}$").WithMessage("Address must start with letters and be between 6 and 20 characters long.");

            RuleFor(x => x.PostalCode).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Postal code is required.")
                    .When(x => x.PostalCode != null && x.PostalCode.Any())
                    .Matches(@"^[0-9]{2,6}$").WithMessage("Postal code must contain only numbers. Maximum of 6 characters long.");

            RuleFor(x => x.Country).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Country is required.")
                    .When(x => x.Country != null && x.Country.Any())
                    .MinimumLength(4).WithMessage("Minimum country name length is 4 characters.")
                    .MaximumLength(60).WithMessage("Maximum country name length is 60 characters.")
                    .Matches(@"^[a-zA-Z ]+$").WithMessage("Country must contain only letters.");

            RuleFor(x => x.City).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("City is required.")
                    .When(x => x.City != null && x.City.Any())
                    .MinimumLength(4).WithMessage("Minimum country name length is 4 characters.")
                    .MaximumLength(30).WithMessage("Maximum country name length is 60 characters.")
                    .Matches(@"^[a-zA-Z ]+$").WithMessage("City must contain only letters.");

        }
    }
}
