using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace msgraphapi
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<TResult> GetContentAs<TResult>(this HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(stringResponse);
        }
    }
}
