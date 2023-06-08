using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatASP.Api.Core;
using ProjekatASP.Api.Core.DTO;
using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.Application.UseCases.Queries;
using ProjekatASP.Implementation;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjekatASP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtManager _manager;
        private readonly UseCaseHandler _handler;

        public UserController(JwtManager manager, UseCaseHandler handler)
        {
            _manager = manager;
            _handler = handler;
        }

        // GET: api/<UserController>

        /// <summary>
        /// Returns filtered users.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /api/users?keyword=c&amp;perPage=10
        /// </remarks>
        /// <response code="200">Returns filtered users</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] PagedSearchDto search, [FromServices] IGetUserQuery query)
        {

            return Ok(_handler.HandleQuery(query, search));
        }

        // POST api/token

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "Email" : "user@gmail.com" <br/>
        ///         "Password" : "Sifra123"
        ///     }
        /// </remarks>
        /// <response code="200">Token generated</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpPost("/api/token")]
        [AllowAnonymous]
        public IActionResult Token([FromBody] TokenRequestDto request)
        {
            try
            {
                var token = _manager.MakeToken(request.Email, request.Password);

                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // POST api/<UserController>

        /// <summary>
        /// Add user
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST 
        ///     {
        ///         "FirstName" : "Mika" <br/>
        ///         "LastName" : "Mikic" <br/>
        ///         "Email" : "mikamikic@gmail.com" <br/>
        ///         "Password" : "Sifra123" <br/>
        ///         "UserName" : "mikamikic123" <br/>
        ///         "Address" : "Adresa 1" <br/>
        ///         "City" : "Grad" <br/>
        ///         "PostalCode" : 11000 <br/>
        ///         "Country" : "Srbija"
        ///     }
        /// </remarks>
        /// <response code="201">Successfully added user</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] CreateUserDto user, [FromServices] ICreateUserCommand command)
        {
            _handler.HandleCommand(command,user);
            return NoContent();
        }

        // PUT api/<UserController>/5

        /// <summary>
        /// Update user
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     PUT 
        ///     {
        ///         "UserId" : 3 <br/>
        ///         "Address": "Adresa 2" <br/>
        ///         "City" : "Grad 2" <br/>
        ///         "PostalCode" : 11200 <br/>
        ///         "Country" : "Drzava 2" <br/>
        ///     }
        /// </remarks>
        /// <response code="204">User updated</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Unexpected Server Error</response>


        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] UpdateUserInfoDto data, [FromServices] IUpdateUserInfoCommand command)
        {
            _handler.HandleCommand(command,data);
            return NoContent();
        }

    }
}
