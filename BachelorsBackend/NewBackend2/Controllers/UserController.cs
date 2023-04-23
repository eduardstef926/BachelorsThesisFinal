using Microsoft.AspNetCore.Mvc;
using NewBackend2.Dtos;
using NewBackend2.Service.Abstract;
using System.ComponentModel.DataAnnotations;

namespace NewBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService coreService)
        {
            this.userService = coreService;
        }

        [HttpPost("AddUserSymptoms")]
        public async Task<IActionResult> AddUserSymptoms(string email, string symptoms)
        {
            if (email == null || symptoms == null)
            {
                return BadRequest("Invalid input");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Invalid object sent from client!");
                    return BadRequest("Invalid user object");
                }

                await userService.AddUserSymptomsAsync(email, symptoms);
                return Ok();

            } catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllSymptoms")]
        public async Task<IActionResult> GetAllSymptoms()
        {
            var symptoms = await userService.GetAllSymptomsAsync();

            if (!symptoms.Any())
            {
                return NoContent();
            }

            return Ok(symptoms);
        }


        [HttpGet("GetLastDiagnosticByUserEmail")]
        public async Task<IActionResult> GetLastDiagnosticByUserEmail(string email)
        {
            if (email == null)
            {
                return BadRequest("Invalid input data");
            }

            var diagnostic = await userService.GetLastDiagnosticByUserEmailAsync(email);

            if (diagnostic == null)
            {
                return NoContent();
            }

            return Ok(diagnostic);
        }

        [HttpGet("GetAppointmentById")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid input data");
            }

            var appointment = await userService.GetAppointmentByIdAsync(id);

            if (appointment == null)
            {
                return NoContent();
            }

            return Ok(appointment);
        }

        [HttpPost("AddAppointmentReview")]
        public async Task<IActionResult> AddAppointmentReview([FromBody] ReviewDto review)
        {
            if (review == null)
            {
                return BadRequest("Invalid input data");
            }

            await userService.AddAppointmentReviewAsync(review);

            return Ok();
        }
    }
}
