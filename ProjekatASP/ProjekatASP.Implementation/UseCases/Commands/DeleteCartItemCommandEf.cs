using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Commands
{
    public class DeleteCartItemCommandEf : UseCaseEf, IDeleteCartItemCommand
    {
        private IUser _user;
        public int UseCaseId => 11;

        public string UseCaseName => "Delete cart item";

        public string UseCaseDescription => "Delete cart item with provided id";

        public DeleteCartItemCommandEf(ProjekatAspDbContext context, IUser user) : base(context)
        {
            _user = user;
        }
        public void Execute(int request)
        {
            var id = _user.Id;

            var userCart = Context.Carts.Where(x => x.UserId == id).Select(x => x.Items).AsQueryable();

            if (!Context.CartItems.Any(x => x.Id == request))
            {
                throw new Exception("Cart item with provided id does not exist.");
            }

            if (!userCart.Any(x => x.Any(x => x.Id == request)))
            {
                throw new Exception("Can't delete cart item from someone elses cart.");
            }

            var item = Context.CartItems.Where(x => x.Id == request).First();

            Context.CartItems.Remove(item);

            Context.SaveChanges();

        }
    }
}
