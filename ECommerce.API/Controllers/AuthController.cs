using ECommerce.Domain.Auth;
using ECommerce.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JWTService _jwtService;

        public AuthController(JWTService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public ActionResult<AuthenticationResponse> Login([FromBody] AuthenticationRequest request)
        {
            var token = _jwtService.GenerateToken(request.Email);

            return Ok(new AuthenticationResponse
            {
                Id = request.Email,
                UserName = request.Email,
                Email = request.Email,
                IsVerified = true,
                JWToken = token,
                Roles = new System.Collections.Generic.List<string> { "Customer" }
            });
        }
    }
}
