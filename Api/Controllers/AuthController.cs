using Application.Abstraction.ExternalServices;
using Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginRequest request)
        {
            var token = _authenticationService.Login(request);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Credenciales inválidas");
            }

            return Ok(token);
        }
    }
}
