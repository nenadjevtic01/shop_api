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
    public class GetSingleReceiptQueryEf : UseCaseEf, IGetSingleReceiptQuery
    {

        private IUser _user;
        public int UseCaseId => 14;

        public string UseCaseName => "Get single receipt.";

        public string UseCaseDescription => "Get single receipt informations.";

        public GetSingleReceiptQueryEf(ProjekatAspDbContext context, IUser user) : base(context)
        {
            _user = user;
        }

        public ReceiptDto Execute(int request)
        {
            var id = _user.Id;

            if (!Context.Receipts.Any(x => x.Id == request))
            {
                throw new EntityNotExistException(nameof(Receipt), request);
            }
            var receipt = new Receipt();
            var role = Context.Users.Where(x => x.Id == _user.Id).Select(x => x.Role).First();
            if (role == 2)
            {
                var usersReceipts = Context.Receipts.Where(x => x.UserId == id);
                if (!usersReceipts.Any(x => x.Id == request))
                {
                    throw new ForbiddenQueryException(_user.Identity);
                }
                receipt = usersReceipts.Where(x => x.Id == request).FirstOrDefault();
            }
            else
            {
                receipt = Context.Receipts.Where(x => x.Id == request).FirstOrDefault();
            }
            return new ReceiptDto
            {
                ReceiptId = receipt.Id,
                Subtotal = receipt.Subtotal,
                Total = receipt.Total,
                ShippingFee = receipt.ShippingFee,
                OrderedAt = receipt.CreatedAt,
                Items = receipt.ReceiptItems.Select(y => new ReceiptItemDto
                {
                    ReceiptItemId= y.Id,
                    ProductName=y.Product.ProductName,
                    Price=y.Price.NewPrice,
                    Category=y.Product.Category.CategoryName,
                    Brand=y.Product.Brand.BrandName,
                    Gender=y.Product.Gender.GenderName,
                    Quantity=y.Quantity
                })
            };
        }
    }
}
