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
    public class ReceiptController : ControllerBase
    {
        private UseCaseHandler _handler;

        public ReceiptController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ReceiptController>

        /// <summary>
        /// Filter receipts
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     GET 
        ///     {
        ///         "FromDate" : "2023-06-06" <br/>
        ///         "ToDate" : "2023-06-08" <br/>
        ///         "UserId" : 3 <br/>
        ///     }
        /// </remarks>
        /// <response code="200">Returns filtered receipts</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromBody] SearchReceiptDto data, [FromServices] IGetReceiptsQuery query)
        {
            return Ok(_handler.HandleQuery(query, data));
            //filtriranje po datumu i korisniku
        }

        // GET api/<ReceiptController>/5

        /// <summary>
        /// Filter receipts
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/cart/id
        /// </remarks>
        /// <response code="200">Returns single receipt</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id, [FromServices] IGetSingleReceiptQuery query)
        {
            return Ok(_handler.HandleQuery(query,id));
        }

        // POST api/<ReceiptController>

        /// <summary>
        /// Confirms order
        /// </summary>
        /// <remarks>
        /// Sample request: POST /api/cart/id
        /// </remarks>
        /// <response code="204"> Order confirmed</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpPost("{id}")]
        [Authorize]
        public IActionResult Post(int id, [FromServices] IConfirmOrderCommand command)
        {
            _handler.HandleCommand(command,id);
            return NoContent();
        }

        // DELETE api/<ReceiptController>/5

        /// <summary>
        /// Deletes receipts
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE /api/cart/id
        /// </remarks>
        /// <response code="204"> Receipt deleted</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteReceiptCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
