using ProjekatASP.Application.Email;
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
    public class ConfirmOrderCommandEf : UseCaseEf, IConfirmOrderCommand
    {
        private IUser _user;
        private IEmail _sender;
        public int UseCaseId => 15;

        public string UseCaseName => "Confirm order";

        public string UseCaseDescription => "Confirm order using user id";

        public ConfirmOrderCommandEf(ProjekatAspDbContext context, IUser user, IEmail sender) : base(context)
        {
            _user = user;
            _sender = sender;
        }
        public void Execute(int request)
        {
            var cart=Context.Carts.Where(x=>x.UserId== request).FirstOrDefault();
            var user = Context.Users.Where(x => x.Id == request).FirstOrDefault();
            if (cart == null)
            {
                throw new EntityNotExistException(nameof(User), request);
            }

            if (request != _user.Id)
            {
                throw new Exception("Can't confirm another user order");
            }

            var cartItems = cart.Items;

            if (cartItems.Count() < 1)
            {
                throw new Exception("Cart is empty.");
            }

            var receipt = new Receipt
            {
                User = user,
                Subtotal = cartItems.Sum(x => x.TotalPrice),
                ShippingFee = cartItems.Sum(x => x.TotalPrice) * 0.2m,
                Total= cartItems.Sum(x => x.TotalPrice) + cartItems.Sum(x => x.TotalPrice) * 0.2m
            };

            var receiptItems = cartItems.Select(x => new ReceiptItem
            {
                Product=x.Product,
                Size=x.Size,
                Price= x.Product.Prices.OrderByDescending(x => x.ActiveFrom).Where(y => y.Product ==x.Product  && y.ActiveFrom < DateTime.UtcNow).First(),
                Quantity=x.Quantity,
                Receipt=receipt
            });

            Context.Receipts.Add(receipt);
            Context.ReceiptsItems.AddRange(receiptItems);
            Context.CartItems.RemoveRange(cartItems);
            Context.SaveChanges();

            _sender.Send(new MessageDto
            {
                To = receipt.User.Email,
                Title = "Order confirmed!",
                Body = "Dear " + receipt.User.Username + "\n Your order is confirmed...."
            });
        }
    }
}
