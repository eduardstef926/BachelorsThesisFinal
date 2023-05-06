using Microsoft.AspNetCore.Mvc;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            this.hospitalService = hospitalService;
        }

        [HttpGet("GetAllHospital")]
        public async Task<IActionResult> GetAllHospitalsPaginated()
        {

            var hospitals = await hospitalService.GetAllHospitalsAsync();

            if (hospitals == null)
            {
                return BadRequest("No hospitals!");
            }

            return Ok(hospitals);
        }
    }
}
