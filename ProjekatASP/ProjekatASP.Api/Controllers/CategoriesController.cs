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
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public CategoriesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<CategoriesController>

        /// <summary>
        /// Returns filtered categories.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/categories?keyword=c&amp;perPage=10
        /// </remarks>
        /// <response code="200">Returns categories</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearchDto search,[FromServices] IGetCategoryQuery query)
        {

            return Ok(_handler.HandleQuery(query, search));
        }

        // POST api/<CategoriesController>

        /// <summary>
        /// Add category
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "Name" : "Category 1" <br/>
        ///     }
        /// </remarks>
        /// <response code="201">Successfully added category</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto data, [FromServices] ICreateCategoryCommand command)
        {
            _handler.HandleCommand(command, data);
            
            return StatusCode(201);
        }
    }
}
