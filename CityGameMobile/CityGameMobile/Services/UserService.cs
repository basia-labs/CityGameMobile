using CityGameMobile.Config;
using CityGameMobile.Extensions;
using CityGameMobile.Models;
using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityGameMobile.Services
{
    public class UserService
    {
        private readonly string baseUrl;

        public UserService()
        {
            baseUrl = Settings.BaseApiUrl + "user";
        }

        public async Task<User> RegisterUserAsync(string username)
        {
            var response = await baseUrl
                .AppendPathSegments("register")
                .SetQueryParam(nameof(username), username)
                .AllowAnyHttpStatus()
                .PostAsync();

            return await response.GetObjectAsync<User>();
        }

        public async Task<User> LoginAsync(string identificator)
        {
            var response = await baseUrl
                .AppendPathSegments("login")
                .SetQueryParam(nameof(identificator), identificator)
                .AllowAnyHttpStatus()
                .PostAsync();

            return await response.GetObjectAsync<User>();
        }

        public async Task<bool> IncreaseScoreAsync(long userId, int score)
        {
            var response = await baseUrl
                .AppendPathSegments("score")
                .SetQueryParam(nameof(userId), userId.ToString())
                .SetQueryParam(nameof(score), score.ToString())
                .AllowAnyHttpStatus()
                .PutAsync();

            return response.StatusCode == 204;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var response = await baseUrl
                .AllowAnyHttpStatus()
                .GetAsync();

            return await response.GetEnumerableAsync<User>();
        }
    }
}