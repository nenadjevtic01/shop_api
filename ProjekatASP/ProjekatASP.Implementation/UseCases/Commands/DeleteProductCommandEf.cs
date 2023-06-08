using ProjekatASP.Application.Exceptions;
using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Commands
{
    public class DeleteProductCommandEf : UseCaseEf, IDeleteProductCommand
    {
        private IUser _user;
        public int UseCaseId => 13;

        public string UseCaseName => "Delete product";

        public string UseCaseDescription => "Delete product with sizes,pictures, prices included.";

        public DeleteProductCommandEf(ProjekatAspDbContext context, IUser user) : base(context)
        {
            _user = user;
        }
        public void Execute(int request)
        {
            var role = Context.Users.Where(x => x.Id == _user.Id).Select(x => x.Role).First();
            if (role == 2)
            {
                throw new Exception("User don't have permission to delete product.");
            }

            var product=Context.Products.Where(x=>x.Id==request).FirstOrDefault();

            if (product == null)
            {
                throw new EntityNotExistException(nameof(Product), request);
            }

            var productSizes = Context.ProductSizes.Where(x => x.ProductId == request).ToList();

            var pictures=Context.Pictures.Where(x=>x.ProductId== request).ToList();

            var prices=Context.Prices.Where(x=>x.ProductId== request).ToList();

            Context.ProductSizes.RemoveRange(productSizes);
            Context.Pictures.RemoveRange(pictures);
            Context.Prices.RemoveRange(prices);
            Context.Products.Remove(product);

            Context.SaveChanges();
        }
    }
}
