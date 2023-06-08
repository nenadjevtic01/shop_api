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
    public class UpdateCartItemCommandEf : UseCaseEf, IUpdateCartItemCommand
    {
        private readonly UpdateCartItemValidator _validator;
        private IUser _user;
        public int UseCaseId => 10;

        public string UseCaseName => "Update cart item.";

        public string UseCaseDescription => "Update cart item details.";

        public UpdateCartItemCommandEf(ProjekatAspDbContext context, UpdateCartItemValidator validator, IUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }
        public void Execute(UpdateCartItemDto request)
        {
            _validator.ValidateAndThrow(request);

            var id = _user.Id;

            if (Context.Carts.Where(x => x.Id == request.CartId).Select(x => x.UserId).First() != id)
            {
                throw new Exception("Can't update someone else cart.");
            }

            var cartItem=Context.CartItems.Where(x=>x.Id==request.CartItemId).FirstOrDefault();
            var productSizes = Context.ProductSizes.Where(x => x.ProductId == request.ProductId).Select(x => x.SizeId);

            if (cartItem==null)
            {
                throw new EntityNotExistException(nameof(CartItem),request.CartItemId);
            }

            if(cartItem.ProductId != request.ProductId)
            {
                throw new Exception("Product id is not the same as the product id of cart item");
            }

            if(cartItem.CartId != request.CartId)
            {
                throw new Exception("Cart id is not the same as the cart id of cart item");
            }

            if (!productSizes.Contains(request.SizeId))
            {
                throw new Exception("Product does not contain size id provided");
            }

            if (cartItem.SizeId != request.SizeId)
            {
                var newItem = Context.CartItems.Where(x => x.CartId == request.CartId && x.ProductId == request.ProductId && x.SizeId == request.SizeId).FirstOrDefault();

                if (newItem == null)
                {
                    var newItemToAdd = new CartItem
                    {
                        CartId = request.CartId,
                        ProductId = request.ProductId,
                        SizeId = request.SizeId,
                        Quantity = request.Quantity,
                        TotalPrice = request.Quantity * Context.Prices.OrderByDescending(x => x.ActiveFrom).Where(x => x.ActiveFrom < DateTime.UtcNow && x.ProductId == request.ProductId).Select(x => x.NewPrice).First()
                    };
                    Context.CartItems.Add(newItemToAdd);
                }
                else
                {
                    newItem.Quantity = newItem.Quantity + request.Quantity;
                    newItem.TotalPrice = newItem.TotalPrice + request.Quantity * Context.Prices.OrderByDescending(x => x.ActiveFrom).Where(x => x.ActiveFrom < DateTime.UtcNow && x.ProductId == request.ProductId).Select(x => x.NewPrice).First();
                    Context.CartItems.Remove(cartItem);
                    Context.CartItems.Update(newItem);
                }
            }
            else
            {
                cartItem.Quantity = cartItem.Quantity + request.Quantity;
                cartItem.TotalPrice = cartItem.TotalPrice + request.Quantity * Context.Prices.OrderByDescending(x => x.ActiveFrom).Where(x => x.ActiveFrom < DateTime.UtcNow && x.ProductId == request.ProductId).Select(x => x.NewPrice).First();
                Context.CartItems.Update(cartItem);
            }

            Context.SaveChanges();
        }
    }
}
