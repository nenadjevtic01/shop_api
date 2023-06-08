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
    public class CreateCartItemCommandEf : UseCaseEf, ICreateCartItemCommand
    {
        private readonly CreateCartItemValidator _validator;

        private IUser _user;

        public int UseCaseId => 9;

        public string UseCaseName => "Add to cart.";

        public string UseCaseDescription => "Add product to cart.";

        public CreateCartItemCommandEf(ProjekatAspDbContext context, CreateCartItemValidator validator, IUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }
        public void Execute(CreateCartItemDto request)
        {
            _validator.ValidateAndThrow(request);

            var id = _user.Id;

            if (Context.Carts.Where(x => x.Id == request.CartId).Select(x => x.UserId).First() != id)
            {
                throw new Exception("Can't add to someone else cart.");
            }

            var price = Context.Prices.OrderByDescending(x=>x.ActiveFrom).Where(x=>x.ProductId==request.ProductId && x.ActiveFrom<DateTime.UtcNow).Select(x=>x.NewPrice).First();
            if(Context.CartItems.Any(x=>x.CartId==request.CartId && x.ProductId==request.ProductId && x.SizeId == request.SizeId))
            {
                var item=Context.CartItems.Where(x=>x.CartId==request.CartId && x.ProductId==request.ProductId && x.SizeId==request.SizeId).First();
                item.Quantity = item.Quantity + request.Quantity;
                item.TotalPrice = item.TotalPrice + request.Quantity * price;
                Context.CartItems.Update(item);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = request.CartId,
                    ProductId = request.ProductId,
                    SizeId = request.SizeId,
                    Quantity = request.Quantity,
                    TotalPrice = request.Quantity * price
                };

                Context.CartItems.Add(cartItem);
            }
            
            Context.SaveChanges();
        }
    }
}
