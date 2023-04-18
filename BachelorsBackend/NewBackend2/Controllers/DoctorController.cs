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

        [HttpGet("GetDoctorByFirstNameAndLastName")]
        public async Task<IActionResult> GetDoctorByFirstNameAndLastName(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                return BadRequest("Invalid input");
            }

            var doctor = await doctorService.GetDoctorByFirstNameAndLastNameAsync(firstName, lastName); 

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

            var collegeDegree = await doctorService.GetDoctorDegreeByFirstNameAndLastNameAsync(firstName, lastName);
            
            if (collegeDegree == null)
            {
                return NoContent();
            }

            return Ok(collegeDegree);
        }

        [HttpGet("GetDoctorReviewsByFirstNameAndLastName")]
        public async Task<IActionResult> GetDoctorReviewsByFirstNameAndLastName(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                return BadRequest("Invalid input");
            }

            var reviews = await doctorService.GetDoctorReviewsByFirstNameAndLastNameAsync(firstName, lastName);

            if (!reviews.Any())
            {
                return NoContent();
            }

            return Ok(reviews);
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
