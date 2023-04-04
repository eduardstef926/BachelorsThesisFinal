using Microsoft.AspNetCore.Mvc;
using NewBackend2.Dtos;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest("Invalid object");
            }

            await authService.Register(user);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var response = await authService.Login(email, password);

            if (response)
            {
                return Ok();
            }

            return BadRequest("Invalid credentials");
        }

        [HttpPut("ModifyPassword")]
        public async Task<IActionResult> ModifyPassword(string id, string newPassword)
        {
            await authService.ModifyPassword(id, newPassword);

            return Ok();
        }

        [HttpPut("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string id)
        {
            await authService.ConfirmEmail(id);

            return Ok();
        }


        [HttpPost("SendForgotPasswordEmail")]
        public async Task<IActionResult> SendForgotPasswordEmail(string email)
        {
            await authService.SendForgotPasswordEmail(email);

            return Ok();
        }
    }
}
