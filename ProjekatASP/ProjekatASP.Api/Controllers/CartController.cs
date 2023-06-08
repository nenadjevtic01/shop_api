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
    public class CartController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CartController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET api/<CartController>/5

        /// <summary>
        /// Returns single cart.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/cart/id
        /// </remarks>
        /// <response code="200">Returns single cart</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id,[FromServices] IGetCartQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<CartController>

        /// <summary>
        /// Add to cart
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "CartId" : 3 <br/>
        ///         "ProductId" : 3 <br/>
        ///         "SizeId" : 2 <br/>
        ///         "Quantity" : 10 <br/>
        ///     }
        /// </remarks>
        /// <response code="201">Successfully added to cart</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateCartItemDto data, [FromServices] ICreateCartItemCommand command)
        {
            _handler.HandleCommand(command, data);
            return NoContent();
        }

        // PUT api/<CartController>/5

        /// <summary>
        /// Update cart item
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     PUT 
        ///     {
        ///         "CartItemId" : 3 <br/>
        ///         "CartId": 2 <br/>
        ///         "ProductId" : 3 <br/>
        ///         "SizeId" : 2 <br/>
        ///         "Quantity" : 10 <br/>
        ///     }
        /// </remarks>
        /// <response code="204">Cart item updated</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] UpdateCartItemDto data, [FromServices] IUpdateCartItemCommand command)
        {
            _handler.HandleCommand(command, data);
            return NoContent();
        }

        // DELETE api/<CartController>/5

        /// <summary>
        /// Delete cart item
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE /api/cart/id
        /// </remarks>
        /// <response code="204">Cart item deleted</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteCartItemCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
