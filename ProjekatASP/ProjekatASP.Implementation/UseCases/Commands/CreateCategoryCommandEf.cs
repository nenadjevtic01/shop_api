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
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Commands
{
    public class CreateCategoryCommandEf : UseCaseEf,ICreateCategoryCommand
    {
        private CreateCategoryValidator _validator;
        private IUser _user;
        public int UseCaseId => 2;
        public string UseCaseName => "Create category";

        public string UseCaseDescription => "Creating new category";

        public CreateCategoryCommandEf(ProjekatAspDbContext context, CreateCategoryValidator validator, IUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }
        public void Execute(CreateCategoryDto request)
        {
            var role = Context.Users.Where(x => x.Id == _user.Id).Select(x => x.Role).First();
            if (role == 2)
            {
                throw new Exception("User don't have permission to add new category.");
            }
            _validator.ValidateAndThrow(request);

            var category = new Category
            {
                CategoryName = request.Name
            };

            Context.Categories.Add(category);

            Context.SaveChanges();

        }
    }
}
