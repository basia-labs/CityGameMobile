using CityGameMobile.Config;
using CityGameMobile.Extensions;
using CityGameMobile.Models;
using Flurl;
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

        public async Task<IEnumerable<Question>> GetQuestionAsync(long localizationId)
        {
            var response = await baseUrl
                .SetQueryParam(nameof(localizationId), localizationId)
                .AllowAnyHttpStatus()
                .GetAsync();

            return await response.GetEnumerableAsync<Question>();
        }
    }
}