using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.Application.UseCases.Queries;
using ProjekatASP.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjekatASP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ProductsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ProductsController>

        /// <summary>
        /// Returns filtered products
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/products?categoryid=2&amp;minimumprice=20&amp;maximumprice=150&amp;perPage=10
        /// </remarks>
        /// <response code="200">Returns filtered products</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] SearchProductDto search, [FromServices] IGetProductQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));
        }

        // GET api/<ProductsController>/5

        /// <summary>
        /// Returns single products.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/products/5
        /// </remarks>
        /// <response code="200">Returns single product</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetSingleProductQuery query)
        {
            return Ok(_handler.HandleQuery(query,id));
        }

        // POST api/<ProductsController>

        /// <summary>
        /// Add product
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "ProductName" : "Product 12" <br/>
        ///         "CategoryId" : 3 <br/>
        ///         "BrandId" : 2 <br/>
        ///         "GenderId" : 1 <br/>
        ///         "Sale" : true <br/>
        ///         "InStock" :true <br/>
        ///         "Material" : "Material 10" <br/>
        ///         "CountryOfOrigin" : "Country 2" <br/>
        ///         "Price" : 
        ///         {
        ///             "Price" : 99,
        ///             "ActiveFrom" : "2023-06-07"
        ///         }<br/>
        ///         "Pictures" : 
        ///         [
        ///         {
        ///             "Src":"slika1.jpg",
        ///             "Alt":"alt1.jpg"
        ///         },
        ///         {
        ///             "Src":"slika2.jpg",
        ///             "Alt":"alt2.jpg"
        ///         }
        ///         ],
        ///         "Sizes" : [1,2,3]
        ///     }
        /// </remarks>
        /// <response code="201">Successfully added</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateProductDto product, [FromServices] ICreateProductCommand command)
        {
            _handler.HandleCommand(command, product);
            return NoContent();
        }


        /// <summary>
        /// Add product price
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "ProductId" : 3 <br/>
        ///         "Price" : 69.99 <br/>
        ///         "ValidFrom" : "2023-06-06" <br/>
        ///     }
        /// </remarks>
        /// <response code="201">Successfully added price</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>


        [HttpPost("/api/addprice")]
        [Authorize]
        public IActionResult PostPrice([FromBody] CreatePriceDto price, [FromServices] IAddProductPriceCommand command)
        {
            _handler.HandleCommand(command, price);
            return NoContent();
        }

        // DELETE api/<ProductsController>/5

        /// <summary>
        /// Delete product
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE /api/products/id
        /// </remarks>
        /// <response code="204">Product deleted</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteProductCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
