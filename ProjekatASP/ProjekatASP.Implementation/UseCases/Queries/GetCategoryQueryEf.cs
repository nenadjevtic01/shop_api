using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.Application.UseCases.Queries;
using ProjekatASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Queries
{
    public class GetCategoryQueryEf : UseCaseEf , IGetCategoryQuery
    {
        public int UseCaseId => 1;

        public string UseCaseName => "Get categories";

        public string UseCaseDescription => "Get categories with/without keyword.";

        public GetCategoryQueryEf(ProjekatAspDbContext context):base(context) 
        {
        
        }

        public PageResponse<CategoryDto> Execute(PagedSearchDto request)
        {
            var query = Context.Categories.Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.CategoryName.Contains(request.Keyword));
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

            var response = new PageResponse<CategoryDto>();
            response.Count = query.Count();
            response.Data = query.Skip(toSkip).Take(request.PerPage.Value).Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.CategoryName,
                Products = x.Products.Select(y => new ProductDto
                {
                    Id= y.Id,
                    ProductName= y.ProductName,
                    Category=y.Category.CategoryName,
                    UnitPrice= y.Prices.OrderByDescending(x => x.ActiveFrom).Where(x => x.ActiveFrom < DateTime.UtcNow).Select(x => x.NewPrice).First()
                }).ToList()
            });
            response.CurrentPage = request.Page.Value;
            response.PerPage = request.PerPage.Value;

            return response;
        }
    }
}
