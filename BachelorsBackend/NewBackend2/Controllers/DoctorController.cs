using Microsoft.AspNetCore.Mvc;
using NewBackend2.Dtos;
using NewBackend2.Service.Abstract;
using System.ComponentModel.DataAnnotations;

namespace NewBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await doctorService.GetAllDoctorsAsync();

            if (!doctors.Any())
            {
                return NoContent();
            }

            return Ok(doctors);
        }

        [HttpGet("GetDoctorLocationsBySpecialization")]
        public async Task<IActionResult> GetDoctorLocationsBySpecialization(string specialization)
        {
            var locations = await doctorService.GetDoctorLocationsBySpecializationAsync(specialization);

            if (!locations.Any())
            {
                return NoContent();
            }

            return Ok(locations);   
        }

        [HttpGet("GetAppointmentDatesByDateSpecializationAndLocation")]
        public async Task<IActionResult> GetAppointmentDatesByDateSpecializationAndLocation(string startDate, string endDate, string location, string specialization)
        {   
            var appointmentSlots = await doctorService.GetAppointmentDatesByDateSpecializationAndLocationAsync(startDate, endDate, location, specialization);   

            if (!appointmentSlots.Any())
            {
                return NoContent();
            }

            return Ok(appointmentSlots);
        }

        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromForm] DoctorDto doctor)
        {
            if (doctor == null)
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

                await doctorService.AddDoctorAsync(doctor);

                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDoctorWithEmploymentByFirstNameAndLastNameAsync")]
        public async Task<IActionResult> GetDoctorWithEmploymentByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                return BadRequest("Invalid input");
            }

            var doctor = await doctorService.GetDoctorWithEmploymentByFirstNameAndLastNameAsync(firstName, lastName); 

            if (doctor == null)
            {
                return NoContent();
            }

            return Ok(doctor);
        }

        [HttpGet("GetDoctorDegreeByFirstNameAndLastName")]
        public async Task<IActionResult> GetDoctorDegreeByFirstNameAndLastName(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                return BadRequest("Invalid input");
            }

            var collegeDegree = await doctorService.GetDoctorDegreesByFirstNameAndLastNameAsync(firstName, lastName);
            
            if (collegeDegree == null)
            {
                return NoContent();
            }

            return Ok(collegeDegree);
        }

        [HttpGet("GetDoctorReviewsPaginatedByFirstNameAndLastName")]
        public async Task<IActionResult> GetDoctorReviewsPaginatedByFirstNameAndLastNameAsync(string firstName, string lastName, int pageIndex)
        {
            if (firstName == null || lastName == null)
            {
                return BadRequest("Invalid input");
            }

            var reviews = await doctorService.GetDoctorReviewsPaginatedByFirstNameAndLastNameAsync(firstName, lastName, pageIndex);

            if (!reviews.Any())
            {
                return NoContent();
            }

            return Ok(reviews);
        }

        [HttpGet("GetDoctorReviewNumbersByFirstNameAndLastName")]
        public async Task<IActionResult> GetDoctorReviewNumbersByFirstNameAndLastName(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                return BadRequest("Invalid input");
            }

            var number = await doctorService.GetDoctorReviewNumberByFirstNameAndLastNameAsync(firstName, lastName);

            return Ok(number);
        }

        [HttpGet("GetDoctorReviewLengthByFirstNameAndLastName")]
        public async Task<IActionResult> GetDoctorReviewLengthByFirstNameAndLastName(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                return BadRequest("Invalid input");
            }

            var result = await doctorService.GetDoctorReviewNumbersByFirstNameAndLastName(firstName, lastName);

            if (result == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("GetDoctorsBySpecialization")]
        public async Task<IActionResult> GetDoctorsBySpecialization(string specialization)
        {
            if (specialization == null)
            {
                return BadRequest("Invalid input");
            }

            var doctors = await doctorService.GetDoctorsBySpecialization(specialization);

            if (!doctors.Any())
            {
                return NoContent();
            }

            return Ok(doctors);
        }
    }
}
