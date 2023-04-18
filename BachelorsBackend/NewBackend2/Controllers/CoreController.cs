using Microsoft.AspNetCore.Mvc;
using NewBackend2.Service.Abstract;
using System.ComponentModel.DataAnnotations;

namespace NewBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreController : ControllerBase
    {
        private readonly ICoreService coreService;

        public CoreController(ICoreService coreService)
        {
            this.coreService = coreService;
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

                await coreService.AddUserSymptomsAsync(email, symptoms);
                return Ok();

            } catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetAllSymptoms")]
        public async Task<IActionResult> GetAllSymptoms()
        {
            var symptoms = await coreService.GetAllSymptomsAsync();

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

            var diagnostic = await coreService.GetLastDiagnosticByUserEmailAsync(email);

            if (diagnostic == null)
            {
                return NoContent();
            }

            return Ok(diagnostic);
        }
    }
}
