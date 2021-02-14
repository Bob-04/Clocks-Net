using System.Threading.Tasks;
using Clocks.Data.Models;
using Clocks.Shared.DtoModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Clocks.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager,
            ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.Username, request.Password, true, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return Accepted();
        }

        [HttpGet("aaa")]
        public async Task<IActionResult> Get()
        {
            return null;
        }
    }
}