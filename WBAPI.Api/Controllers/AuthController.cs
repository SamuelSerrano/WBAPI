using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WBAPI.Api.Services;


namespace WBAPI.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _token;
        public AuthController(ITokenService token) => _token = token;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            if (req.Username == "admin" && req.Password == "password")
            {
                var jwt = _token.GenerateToken(req.Username);
                return Ok(new { token = jwt });
            }
            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
