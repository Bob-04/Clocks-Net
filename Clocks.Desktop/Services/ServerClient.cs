using System.Net.Http;
using System.Threading.Tasks;
using Clocks.Shared.DtoModels;

namespace Clocks.Desktop.Services
{
    internal class ServerClient : IServerClient
    {
        private readonly HttpClient _httpClient;

        public ServerClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> SignIn(string login, string password)
        {

            return true;
        }

        public async Task<bool> SignUp(UserDto user)
        {

            return true;
        }
    }
}
