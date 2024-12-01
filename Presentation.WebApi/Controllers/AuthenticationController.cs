using Application.DTOs.Auth;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authService = authenticationService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var result = await _authService.Signup(userForRegistration);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authService.ValidateUser(user))
                return Unauthorized();

            return Ok(new
            {
                Token = await _authService.CreateToken()
            });
        }
    }
}
