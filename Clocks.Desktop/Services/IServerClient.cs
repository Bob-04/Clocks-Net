using System.Threading.Tasks;
using Clocks.Shared.DtoModels;
using Clocks.Shared.DtoModels.Account;

namespace Clocks.Desktop.Services
{
    internal interface IServerClient
    {
        Task<UserDto> SignIn(SignInRequest request);
        Task<UserDto> SignUp(SignUpRequest request);
    }
}
