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
        public async Task<IActionResult> LogOut()
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

        [HttpPost("AddUserSubscription")]
        public async Task<IActionResult> AddUserSubscription([FromBody] SubscriptionInputDto subscription)
        {
            if (subscription == null)
            {
                return BadRequest("Invalid input data");
            }

            await userService.AddUserSubscriptionAsync(subscription);

            return Ok();
        }


        [HttpPost("ScheduleAppointment")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] AppointmentDto appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Invalid input");
            }

            await userService.ScheduleAppointment(appointment);

            return Ok();
        }

        [HttpGet("CheckUserSubscription")]
        public async Task<IActionResult> CheckUserSubscription(string email)
        {
            if (email == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await userService.CheckUserSubscriptionAsync(email);

            return Ok(result);
        }

        [HttpGet("GetFullUserDataByEmail")]
        public async Task<IActionResult> GetFullUserDataByEmail(string email)
        {
            if (email == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await userService.GetFullUserDataByEmailAsync(email);

            return Ok(result);
        }

        [HttpGet("GetUserSubscription")]
        public async Task<IActionResult> GetUserSubscription(string email)
        {
            if (email == null)
            {
                return BadRequest("Invalid input!");
            }

            var result = await userService.GetUserSubscriptionAsync(email);

            return Ok(result);
        }

        [HttpPut("UpdateUserData")] 
        public async Task<IActionResult> UpdateUserData(string firstName, string lastName, string email, int phoneNumber)
        {
            if (firstName == null || lastName == null || email == null || phoneNumber == 0)
            {
                return BadRequest("Invalid input data");
            }

            await userService.UpdateUserDataAsync(firstName, lastName, email, phoneNumber);
            
            return Ok();
        }

        [HttpGet("GetUserAppointments")]
        public async Task<IActionResult> GetUserAppointments(string email)
        {
            if (email == null)
            {
                return BadRequest("Invalid object");
            }

            var appointments = await userService.GetUserAppointmentsByEmailAsync(email);

            return Ok(appointments);
        }
    }
}
