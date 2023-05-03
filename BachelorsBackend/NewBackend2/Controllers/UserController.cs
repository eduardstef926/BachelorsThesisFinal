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
        public async Task<IActionResult> AddUserSymptoms(int cookieId, string symptoms)
        {
            if (symptoms == null)
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

                await userService.AddUserSymptomsAsync(cookieId, symptoms);
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

        [HttpGet("GetLastDiagnosticBySessionId")]
        public async Task<IActionResult> GetLastDiagnosticBySessionId(int cookieId)
        {
            if (cookieId == 0)
            {
                return BadRequest("Invalid input data");
            }

            var diagnostic = await userService.GetLastDiagnosticBySessionIdAsync(cookieId);

            if (diagnostic == null)
            {
                return NoContent();
            }

            return Ok(diagnostic);
        }

        [HttpGet("GetAppointmentById")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            if (id == 0)
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
        public async Task<IActionResult> CheckUserSubscription(int cookieId)
        {
            if (cookieId == 0)
            {
                return BadRequest("Invalid input");
            }

            var result = await userService.CheckUserSubscriptionAsync(cookieId);

            return Ok(result);
        }

        [HttpDelete("CancelUserSubscription")]
        public async Task<IActionResult> CancelUserSubscription(int cookieId)
        {
            if (cookieId == 0)
            {
                return BadRequest("Invalid input");
            }

            await userService.CancelUserSubscriptionAsync(cookieId);

            return Ok();
        }

        [HttpGet("GetFullUserDataByCookieId")]
        public async Task<IActionResult> GetFullUserDataByCookieId(int cookieId)
        {
            if (cookieId == 0)
            {
                return BadRequest("Invalid input");
            }

            var result = await userService.GetFullUserDataByCookieIdAsync(cookieId);

            return Ok(result);
        }

        [HttpGet("GetUserSubscription")]
        public async Task<IActionResult> GetUserSubscription(string email)
        {
            if (email == null)
            {
                return BadRequest("Invalid input!");
            }

            var subscription = await userService.GetUserSubscriptionAsync(email);

            if (subscription == null)
            {
                return BadRequest("No subscription");
            }

            return Ok(subscription);
        }

        [HttpPut("UpdateUserData")] 
        public async Task<IActionResult> UpdateUserData([FromBody] FullUserDataDto user)
        {
            if (user == null)
            {
                return BadRequest("Invalid input data");
            }

            await userService.UpdateUserDataAsync(user);

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

            if (appointments.Count == 0)
            {
                return BadRequest("No Appointments");
            }

            return Ok(appointments);
        }
    }
}
