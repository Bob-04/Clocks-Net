using System.Threading.Tasks;
using Clocks.Data.Models;
using Clocks.Shared.DtoModels;
using Clocks.Shared.DtoModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clocks.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.Username, request.Password, true, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var user = await _userManager.GetUserAsync(User);

            return Accepted(new UserDto {Username = user.UserName});
        }

        [HttpGet("aaa")]
        public async Task<IActionResult> Get()
        {
            return null;
        }
    }
}