using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjekatASP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class InitialController : ControllerBase
    {
        private UseCaseHandler _handler;

        public InitialController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // POST api/<InitialController>

        /// <summary>
        /// Initial insert to database
        /// </summary>
        /// <response code="204">Successfully inserted to database</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpPost]
        public IActionResult Post([FromServices] IFillDatabaseCommand command)
        {
            _handler.HandleCommand(command,0);
            return NoContent();
        }

    }
}
