using FluentValidation;
using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using ProjekatASP.Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Commands
{
    public class CreateUserCommandEf : UseCaseEf, ICreateUserCommand
    {
        private CreateUserValidator _validator;

        public int UseCaseId => 4;

        public string UseCaseName => "Create new user";

        public string UseCaseDescription => "Create new user description";

        public CreateUserCommandEf(ProjekatAspDbContext context, CreateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public void Execute(CreateUserDto request)
        {
            
            _validator.ValidateAndThrow(request);

            var info = new Info
            {
                Address=request.Address,
                City=request.City,
                PostalCode=request.PostalCode,
                Country=request.Country
            };

            var cart = new Cart();

            var useCases = new List<UserUseCase>
            {
                new UserUseCase
                {
                    UseCaseId=1
                },
                new UserUseCase
                {
                    UseCaseId=2
                },
                new UserUseCase
                {
                    UseCaseId=3
                },
                new UserUseCase
                {
                    UseCaseId=4
                },
                new UserUseCase
                {
                    UseCaseId=5
                },
                new UserUseCase
                {
                    UseCaseId=6
                },
                new UserUseCase
                {
                    UseCaseId=7
                },
                new UserUseCase
                {
                    UseCaseId=8
                },
                new UserUseCase
                {
                    UseCaseId=9
                },
                new UserUseCase
                {
                    UseCaseId=10
                },
                new UserUseCase
                {
                    UseCaseId=12
                },
                new UserUseCase
                {
                    UseCaseId=13
                },
                new UserUseCase
                {
                    UseCaseId=16
                },
                new UserUseCase
                {
                    UseCaseId=17
                }
            };

            var user = new User
            {
                FirstName=request.FirstName,
                LastName=request.LastName,
                Email=request.Email,
                Username=request.UserName,
                Password=BCrypt.Net.BCrypt.HashPassword(request.Password),
                IsBanned=false,
                Info=info,
                Cart=cart,
                UserUseCases=useCases
            };

            //Context.UserUseCases.AddRange(user.UserUseCases);
            Context.Infos.Add(info);
            Context.Carts.Add(cart);
            Context.Users.Add(user);

            Context.SaveChanges();
        }
    }
}
