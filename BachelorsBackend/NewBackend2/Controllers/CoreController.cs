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

        [HttpPost("GetSymptomData")]
        public async Task<IActionResult> GetSymptomData(string email, string symptoms)
        {
            var symptomArray = symptoms.Split('\u002C').ToList();

            await coreService.GetSymptomDataAsync(email, symptomArray);

            return Ok();
        }
    }
}
