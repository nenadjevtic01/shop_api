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
    public class DeleteReceiptCommandEf : UseCaseEf, IDeleteReceiptCommand
    {
        private IUser _user;
        public int UseCaseId => 17;

        public string UseCaseName => "Delete receipt";

        public string UseCaseDescription => "Delete receipt with id provided.";

        public DeleteReceiptCommandEf(ProjekatAspDbContext context, IUser user) : base(context)
        {
            _user = user;
        }
        public void Execute(int request)
        {
            var role=Context.Users.Where(x=>x.Id==_user.Id).Select(x=>x.Role).First();
            var receipt = Context.Receipts.Where(x => x.Id == request).FirstOrDefault();
            if(receipt == null)
            {
                throw new EntityNotExistException(nameof(Receipt), request);
            }

            if (role == 2)
            {
                if(!Context.Receipts.Any(x=>x.Id==request && x.UserId == _user.Id))
                {
                    throw new Exception("Can't delete another user receipt.");
                }
            }

            var receiptItems = receipt.ReceiptItems;

            Context.ReceiptsItems.RemoveRange(receiptItems);
            Context.Receipts.Remove(receipt);
            Context.SaveChanges();

            
        }
    }
}
