using Microsoft.AspNetCore.Mvc;
using NewBackend2.Dtos;
using NewBackend2.Service.Abstract;
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
            if (id == null || newPassword == null)
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

                await authService.ModifyPassword(id, newPassword);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string id)
        {
            if (id == null)
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

                await authService.ConfirmEmail(id);

                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("SendForgotPasswordEmail")]
        public async Task<IActionResult> SendForgotPasswordEmail(string email)
        {
            if (email == null)
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

                await authService.SendForgotPasswordEmail(email);

                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
