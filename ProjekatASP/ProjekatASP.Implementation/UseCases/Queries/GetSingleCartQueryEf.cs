using ProjekatASP.Application.Exceptions;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.Application.UseCases.Queries;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Queries
{
    public class GetSingleCartQueryEf : UseCaseEf, IGetCartQuery
    {
        private IUser _user;
        public int UseCaseId => 8;

        public string UseCaseName => "Get single cart";

        public string UseCaseDescription => "Get single cart details.";

        public GetSingleCartQueryEf(ProjekatAspDbContext context, IUser user) : base(context)
        {
            _user = user;
        }
        public CartDto Execute(int request)
        {
            var id=_user.Id;

            var role = Context.Users.Where(x => x.Id == _user.Id).Select(x => x.Role).First();
            if (role == 2)
            {
                if (id != request)
                {
                    throw new Exception("Can't see another user cart.");
                }
            }
            var cart=Context.Carts.Where(x=>x.Id==request).FirstOrDefault();

            if (cart == null)
            {
                throw new EntityNotExistException(nameof(Cart), request);
            }

            return new CartDto
            {
                CartId = cart.Id,
                Items = cart.Items.Select(x => new CartItemDto
                {
                    CartItemId= x.Id,
                    ProductName=x.Product.ProductName,
                    Quantity= x.Quantity,
                    Size=x.Size.SizeName,
                    TotalPrice= x.Quantity* x.Product.Prices.OrderByDescending(x => x.ActiveFrom).Where(x => x.ActiveFrom < DateTime.UtcNow).Select(x => x.NewPrice).First()
                })
            };
        }
    }
}
