using CityGameMobile.Config;
using CityGameMobile.Extensions;
using CityGameMobile.Models;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityGameMobile.Services
{
    public class QuestionService
    {
        private readonly string baseUrl;

        public QuestionService()
        {
            baseUrl = Settings.BaseApiUrl + "question";
        }

        public async Task<IEnumerable<Question>> GetQuestionAsync()
        {
            var response = await baseUrl
                .AllowAnyHttpStatus()
                .GetAsync();

            return await response.GetEnumerableAsync<Question>();
        }
    }
}