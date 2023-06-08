using FluentValidation;
using ProjekatASP.Application.Exceptions;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.Application.UseCases.Queries;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using ProjekatASP.Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Queries
{
    public class GetReceiptsQueryEf : UseCaseEf, IGetReceiptsQuery
    {
        private IUser _user;
        public int UseCaseId => 16;

        public string UseCaseName => "Search receipts";

        public string UseCaseDescription => "Search receipts using dates and user id";

        public GetReceiptsQueryEf(ProjekatAspDbContext context, IUser user) : base(context)
        {
            _user = user;
        }
        public PageResponse<ReceiptDto> Execute(SearchReceiptDto request)
        {
            var id = _user.Id;
            var role = Context.Users.Where(x => x.Id == id).Select(x => x.Role).First();
            var receipts = Context.Receipts.AsQueryable();
            if (request.UserId == null && role==2)
            {
                request.UserId = id;
                receipts = Context.Receipts.Where(x => x.UserId == request.UserId);
            }

            if (!Context.Users.Any(x => x.Id == request.UserId))
            {
                throw new Exception("User with provided id does not exist.");
            }

            if (role==2 && request.UserId != id)
            {
                throw new Exception("User tried to search another user receipts.");
            }

            if (!receipts.Any())
            {
                throw new EntityNotExistException(nameof(Receipt), id);
            }

            if (request.FromDate != null)
            {
                receipts.Where(x => x.CreatedAt > request.FromDate);
            }

            if(request.ToDate != null)
            {
                receipts.Where(x=>x.CreatedAt< request.ToDate);
            }

            if (request.PerPage == null || request.PerPage < 1)
            {
                request.PerPage = 15;
            }

            if (request.Page == null || request.Page < 1)
            {
                request.PerPage = 1;
            }

            var toSkip = (request.Page.Value - 1) * request.PerPage.Value;

            var response = new PageResponse<ReceiptDto>();
            response.Count = receipts.Count();
            response.Data = receipts.Skip(toSkip).Take(request.PerPage.Value).Select(x => new ReceiptDto
            {
                ReceiptId = x.Id,
                Subtotal = x.Subtotal,
                ShippingFee = x.ShippingFee,
                Total = x.Total,
                OrderedAt = x.CreatedAt,
                Items = x.ReceiptItems.Select(x => new ReceiptItemDto
                {
                    ReceiptItemId=x.Id,
                    ProductName=x.Product.ProductName,
                    Brand=x.Product.Brand.BrandName,
                    Price=x.Price.NewPrice,
                    Gender=x.Product.Gender.GenderName,
                    Category=x.Product.Category.CategoryName,
                    Quantity=x.Quantity
                })
            }).ToList();

            response.CurrentPage = request.Page.Value;
            response.PerPage = request.PerPage.Value;

            return response;
        }
    }
}
