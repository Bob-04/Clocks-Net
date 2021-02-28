using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Clocks.Shared.DtoModels;
using Clocks.Shared.DtoModels.Account;
using Newtonsoft.Json;

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

        public async Task<UserDto> SignIn(SignInRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/signin", request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await DeserializeResponse<UserDto>(response);
        }

        public async Task<UserDto> SignUp(SignUpRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/signup", request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await DeserializeResponse<UserDto>(response);
        }

        public async Task<IEnumerable<ClockDto>> GetUserClocks()
        {
            var response = await _httpClient.GetAsync("api/clocks");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await DeserializeResponse<IEnumerable<ClockDto>>(response);
        }

        private static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
