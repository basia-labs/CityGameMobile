using CityGameMobile.Config;
using CityGameMobile.Extensions;
using CityGameMobile.Models;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityGameMobile.Services
{
    public class LocalizationService
    {
        private readonly string baseUrl;

        public LocalizationService()
        {
            baseUrl = Settings.BaseApiUrl + "localization";
        }

        public async Task<IEnumerable<Localization>> GetLocalizationsAsync()
        {
            var response = await baseUrl
                .AllowAnyHttpStatus()
                .GetAsync();

            return await response.GetEnumerableAsync<Localization>();
        }
    }
}