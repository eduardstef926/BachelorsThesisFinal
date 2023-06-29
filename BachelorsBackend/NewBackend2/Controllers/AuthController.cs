using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewBackend2.Dtos;
using NewBackend2.Service.Abstract;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> Login(LoggedUserDto user)
        {
            if (user == null)
            {
                return BadRequest("Invalid object");
            }

            var response = await authService.Login(user);

            if (response == -1)
            {
                return BadRequest("Invalid username or password!");
            }

            return Ok(response);
        }

        [HttpGet("CheckLoginCookie")]
        public async Task<IActionResult> CheckLoginCookie(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid object");
            }

            var response = await authService.CheckLoginCookie(id);

            if (response == false)
            {
                return BadRequest("No cookies!");
            }

            return Ok(response);
        }

        [HttpPut("ModifyPassword")]
        public async Task<IActionResult> ModifyPassword(int id, string newPassword)
        {
            if (id == 0 || newPassword == null)
            {
                return BadRequest("Invalid object");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Invalid object sent from client!");
                    return BadRequest("Invalid user object");
                }

                var result = await authService.ModifyPassword(id, newPassword);

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, int code)
        {
            if (email == null)
            {
                return BadRequest("Invalid object");
            }

            var response = await authService.ConfirmEmail(email, code);

            if (response == false)
            {
                return BadRequest("Invalid code");
            }

            return Ok();
        }

        [HttpDelete("LogOut")]
        public async Task<IActionResult> LogOut(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid input data");
            }

            await authService.LogOut(id);

            return Ok();
        }
    }
}
