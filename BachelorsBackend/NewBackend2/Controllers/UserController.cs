using Microsoft.AspNetCore.Mvc;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();

            if (!users.Any())
            {
                return NoContent();
            }

            return Ok(users);
        }
    }
}
