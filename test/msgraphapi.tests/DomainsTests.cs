using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace msgraphapi.tests
{
    public class DomainsTests
    {

        private readonly HttpClient _httpClient;

        public DomainsTests()
        {
            _httpClient = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()).CreateClient();
        }

        [Fact]
        public async Task GetAllDomains()
        {
            var response = await _httpClient.GetAsync("/domains");
            // var result = await response.GetContentAs<List<Domain>>();
            // Assert.True(result.Any());
        }
    }
}
