using Microsoft.AspNetCore.Mvc;
using NewBackend2.Dtos;
using NewBackend2.Service.Abstract;
using System.ComponentModel.DataAnnotations;

namespace NewBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineerController : ControllerBase
    {
        private readonly IEngineerService engineerService;

        public EngineerController(IEngineerService engineerService)
        {
            this.engineerService = engineerService;
        }

        [HttpGet("GetAllEngineers")]
        public async Task<IActionResult> GetAllEngineers()
        {
            var engineers = await engineerService.GetAllEngineersAsync();

            if (!engineers.Any())
            {
                return NoContent();
            }

            return Ok(engineers);
        }

        [HttpPost("AddEngineer")]
        public async Task<IActionResult> AddDoctor([FromForm] EngineerDto engineer)
        {
            if (engineer == null)
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

                await engineerService.AddEngineerAsync(engineer);

                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
