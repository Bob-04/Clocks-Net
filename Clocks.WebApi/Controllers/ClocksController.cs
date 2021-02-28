using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clocks.Data;
using Clocks.Data.Models;
using Clocks.Shared.DtoModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clocks.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClocksController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _dbContext;

        public ClocksController(UserManager<User> userManager, ApplicationContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ClockDto>> GetUserClocks()
        {
            var user = await _userManager.GetUserAsync(User);
            var clocks = await _dbContext.Clocks
                .Where(c => c.UserId == user.Id)
                .Select(c => new ClockDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    TimeZoneId = c.TimeZoneId
                })
                .ToListAsync();

            return clocks;
        }
    }
}
