using Microsoft.EntityFrameworkCore;
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
    public class GetSingleProductQueryEf : UseCaseEf, IGetSingleProductQuery
    {
        public int UseCaseId => 7;

        public string UseCaseName => "Get single product";

        public string UseCaseDescription => "Detailed product";

        public GetSingleProductQueryEf(ProjekatAspDbContext context): base(context)
        {

        }

        public SingleProductDto Execute(int request)
        {
            var product = Context.Products.Include(x => x.Gender).Include(x => x.Category).Include(x => x.Brand).Include(x => x.Prices).Include(x => x.Pictures).FirstOrDefault(x => x.Id == request && x.IsActive);

            if (product == null)
            {
                throw new EntityNotExistException(nameof(Product), request);
            }

            return new SingleProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Brand = product.Brand.BrandName,
                Gender = product.Gender.GenderName,
                Category = product.Category.CategoryName,
                Sale = product.Sale,
                InStock = product.InStock,
                Material = product.Material,
                CountryOfOrigin = product.CountryOfOrigin,
                NewPrice = product.Prices.OrderByDescending(x => x.ActiveFrom).Where(x => x.ActiveFrom < DateTime.UtcNow).Select(x => x.NewPrice).First(),
                OldPrice = product.Prices.OrderByDescending(x => x.ActiveFrom).Where(x => x.ActiveFrom < DateTime.UtcNow).Select(x => x.OldPrice).First(),
                Pictures =product.Pictures.Select(x=> new PictureDto
                {
                    Src=x.Src,
                    Alt=x.Alt
                }).ToList(),
                Sizes=product.ProductSizes.Select(x=>x.Size.SizeName).ToList()
            };
        }
    }
}
