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
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        private readonly ProjekatAspDbContext _context;

        public CreateUserValidator(ProjekatAspDbContext context)
        {
            _context = context;
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(3).WithMessage("Minimum first name length is 3 characters.")
                .MaximumLength(20).WithMessage("Maximum first name length is 20 characters.")
                .Matches(@"^[A-Za-z ]+$").WithMessage("First name must have only letters.");

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(3).WithMessage("Minimum last name length is 3 characters.")
                .MaximumLength(20).WithMessage("Maximum last name length is 20 characters.")
                .Matches(@"^[A-Za-z ]+$").WithMessage("Last name must have only letters.");

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email invalid.")
                .Must(email => !_context.Users.Any(u => u.Email == email && u.IsActive)).WithMessage("Email already in use.");

            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(6).WithMessage("Minimum username length is 6 characters.")
                .MaximumLength(20).WithMessage("Maximum username length is 20 characters.")
                .Matches(@"^(?=[a-z0-9._]{6,20}$)(?!.*[_.]{2})[^_.].*[^_.]$").WithMessage("Username must have between 6 and 20 characters. Can't start or end with . or _. Can't have .. or ._ or __ inside.")
                .Must(username => !_context.Users.Any(u => u.Username == username && u.IsActive)).WithMessage("Username already in use.");

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Minimum eight characters, at least one letter and one number.");

            RuleFor(x => x.Address).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Address is required.")
                    .Matches(@"^[a-zA-Z0-9 ]{6,20}$").WithMessage("Address must start with letters and be between 6 and 20 characters long.");

            RuleFor(x => x.PostalCode).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Postal code is required.")
                    .Matches(@"^[0-9]{2,6}$").WithMessage("Postal code must contain only numbers. Maximum of 6 characters long.");

            RuleFor(x => x.Country).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Country is required.")
                    .MinimumLength(4).WithMessage("Minimum country name length is 4 characters.")
                    .MaximumLength(60).WithMessage("Maximum country name length is 60 characters.")
                    .Matches(@"^[a-zA-Z ]+$").WithMessage("Country must contain only letters.");

            RuleFor(x => x.City).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("City is required.")
                    .Matches(@"^[a-zA-Z ]+$").WithMessage("City must contain only letters.");


            
        }
    }
}
