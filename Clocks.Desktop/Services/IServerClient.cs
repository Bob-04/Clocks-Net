using System.Threading.Tasks;
using Clocks.Shared.DtoModels;

namespace Clocks.Desktop.Services
{
    internal interface IServerClient
    {
        Task<UserDto> SignIn(string login, string password);
        Task<UserDto> SignUp(string login, string password);
    }
}
