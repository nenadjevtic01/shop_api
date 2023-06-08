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
    public class AddProductPriceCommandEf : UseCaseEf, IAddProductPriceCommand
    {
        private IUser _user;
        private AddPriceValidator _validator;
        public int UseCaseId => 19;

        public string UseCaseName => "Add product price";

        public string UseCaseDescription => "Add new product price";

        public AddProductPriceCommandEf(ProjekatAspDbContext context, IUser user, AddPriceValidator validator) : base(context)
        {
            _user = user;
            _validator = validator;
        }

        public void Execute(CreatePriceDto request)
        {
            _validator.ValidateAndThrow(request);

            var prices=Context.Prices.Where(x=>x.ProductId==request.ProductId).ToList();

            if(prices.Any(x=>x.ProductId==request.ProductId && x.NewPrice==request.Price && x.ActiveFrom >= request.ValidFrom))
            {
                throw new Exception("Price with same value and active from date is already inserted.");
            }

            var role = Context.Users.Where(x => x.Id == _user.Id).Select(x => x.Role).First();
            if (role == 2)
            {
                throw new Exception("User can't add price.");
            }

            var price = new Price
            {
                NewPrice= request.Price,
                ActiveFrom= request.ValidFrom,
                Product=Context.Products.Where(x=>x.Id==request.ProductId).First()
            };

            Context.Prices.Add(price);
            Context.SaveChanges();
        }
    }
}
