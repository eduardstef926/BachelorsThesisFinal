using Microsoft.AspNetCore.Mvc;
using NewBackend2.Service.Abstract;
using System.ComponentModel.DataAnnotations;

namespace NewBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPut("SendEmailConfirmation")]
        public async Task<IActionResult> SendEmailConfirmation(string email)
        {
            if (email == null)
            {
                return BadRequest("Invalid object");
            }

            await emailService.SendEmailConfirmationAsync(email);

            return Ok();
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

                await emailService.SendForgotPasswordEmailAsync(email);

                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
