using Microsoft.AspNetCore.Mvc;
using NewBackend2.Service.Abstract;

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
            var symptomArray = symptoms.Split('\u002C').ToList();

            await coreService.AddUserSymptomsAsync(email, symptomArray);

            return Ok();
        }

        [HttpPost("GetAllSymptoms")]
        public async Task<IActionResult> GetAllSymptoms()
        {
            var symptoms = await coreService.GetAllSymptomsAsync();

            return Ok(symptoms);
        }
    }
}
