using System.Threading.Tasks;
using Clocks.Shared.DtoModels;

namespace Clocks.Desktop.Services
{
    internal interface IServerClient
    {
        Task<bool> SignIn(string login, string password);
        Task<bool> SignUp(UserDto user);
    }
}
