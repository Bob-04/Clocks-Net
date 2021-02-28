using System.Collections.Generic;
using System.Threading.Tasks;
using Clocks.Shared.DtoModels;
using Clocks.Shared.DtoModels.Account;

namespace Clocks.Desktop.Services
{
    internal interface IServerClient
    {
        Task<UserDto> SignIn(SignInRequest request);
        Task<UserDto> SignUp(SignUpRequest request);
        Task SignOut();

        Task<IEnumerable<ClockDto>> GetUserClocks();
        Task<bool> AddClock(ClockDto clock);
        Task<bool> EditClock(ClockDto clock);
        Task<bool> RemoveClock(ClockDto clock);
    }
}
