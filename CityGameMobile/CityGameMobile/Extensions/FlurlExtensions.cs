using Flurl.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGameMobile.Extensions
{
    public static class FlurlExtensions
    {
        public static async Task<T> GetObjectAsync<T>(this IFlurlResponse response) where T : class, new()
        {
            return response.StatusCode == 200 ? await response.GetJsonAsync<T>() : new T();
        }

        public static async Task<IEnumerable<T>> GetEnumerableAsync<T>(this IFlurlResponse response) where T : new()
        {
            return response.StatusCode == 200
                ? await response.GetJsonAsync<IEnumerable<T>>()
                : Enumerable.Empty<T>();
        }
    }
}