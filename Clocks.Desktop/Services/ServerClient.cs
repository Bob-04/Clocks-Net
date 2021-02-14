using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Clocks.Shared.DtoModels;
using Clocks.Shared.DtoModels.Account;

namespace Clocks.Desktop.Services
{
    internal class ServerClient : IServerClient
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://localhost:44373/";

        public ServerClient()
        {
            _httpClient = new HttpClient {BaseAddress = new Uri(ApiUrl)};
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<UserDto> SignIn(string login, string password)
        {
            var request = new SignInRequest
            {
                Username = login,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("api/accounts/signin", request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDto>(responseString);
        }

        public async Task<UserDto> SignUp(string login, string password)
        {
            return null;
        }
    }
}
