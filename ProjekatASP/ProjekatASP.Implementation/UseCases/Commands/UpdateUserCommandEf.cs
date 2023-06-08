using FluentValidation;
using ProjekatASP.Application.Exceptions;
using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using ProjekatASP.Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Commands
{
    public class UpdateUserCommandEf : UseCaseEf, IUpdateUserInfoCommand
    {
        private readonly UpdateUserInfoValidator _validator;
        private IUser _user;
        public int UseCaseId => 6;

        public string UseCaseName => "Update user info";

        public string UseCaseDescription => "Update user information(Address, City, etc.)";

        public UpdateUserCommandEf(ProjekatAspDbContext context, UpdateUserInfoValidator validator, IUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }
        public void Execute(UpdateUserInfoDto request)
        {
            _validator.ValidateAndThrow(request);

            var id=_user.Id;

            var role = Context.Users.Where(x => x.Id == _user.Id).Select(x => x.Role).First();
            if (role == 2)
            {
                if (request.UserId != id)
                {
                    throw new Exception("Can't update information for another user.");
                }
            }

            var info=Context.Infos.Where(x=>x.UserId==request.UserId).FirstOrDefault();

            if(info != null)
            {
                if(request.Address!=null && request.Address != info.Address)
                {
                    info.Address = request.Address;
                }

                if (request.City != null && request.City != info.City)
                {
                    info.City = request.City;
                }

                if (request.Country != null && request.Country != info.Country)
                {
                    info.Country = request.Country;
                }

                if(request.PostalCode!=null && request.PostalCode!= info.PostalCode)
                {
                    info.PostalCode = request.PostalCode;
                }

                Context.Infos.Update(info);
            }
            else
            {
                throw new EntityNotExistException(nameof(Info), request.UserId);
            }
      
            Context.SaveChanges();
        }
    }
}
