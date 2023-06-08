using Microsoft.EntityFrameworkCore;
using ProjekatASP.Application.UseCases;
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
    public class GetProductsQueryEf : UseCaseEf, IGetProductQuery
    {
        public int UseCaseId => 5;

        public string UseCaseName => "Get products";

        public string UseCaseDescription => "Search and filter products.";

        public GetProductsQueryEf(ProjekatAspDbContext context) : base(context)
        {

        }

        public PageResponse<ProductDto> Execute(SearchProductDto request)
        {
            var query = Context.Products.Include(x => x.Category).Include(x => x.Prices).AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.ProductName.Contains(request.Keyword));
            }

            if (request.CategoryId != null && Context.Categories.Any(x=>x.Id== request.CategoryId))
            {
                query = query.Where(x=>x.CategoryId==request.CategoryId);
            }

            if (request.MinimumPrice != null)
            {
                query = query.Where(x => x.Prices.OrderByDescending(x=>x.ActiveFrom).Where(x => x.ActiveFrom < DateTime.UtcNow).Select(x=>x.NewPrice).First() > request.MinimumPrice);
            }

            if (request.MaximumPrice != null)
            {
                query = query.Where(x => x.Prices.OrderByDescending(x => x.ActiveFrom).Where(x=>x.ActiveFrom<DateTime.UtcNow).Select(x => x.NewPrice).First() < request.MaximumPrice);
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

            var response = new PageResponse<ProductDto>();
            response.Count = query.Count();
            response.Data = query.Skip(toSkip).Take(request.PerPage.Value).Select(x => new ProductDto
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Category = x.Category.CategoryName,
                UnitPrice =x.Prices.OrderByDescending(x => x.ActiveFrom).Where(x=>x.ActiveFrom<DateTime.UtcNow).Select(x => x.NewPrice).First()
            }).ToList();

            response.CurrentPage = request.Page.Value;
            response.PerPage = request.PerPage.Value;

            return response;
        }
    }
}
