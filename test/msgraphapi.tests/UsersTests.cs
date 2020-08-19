using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace msgraphapi.tests
{
    public class UsersTests
    {

        private readonly HttpClient _httpClient;

        public UsersTests()
        {
            _httpClient = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()).CreateClient();
        }

        [Fact]
        public async Task GetAllUsers()
        {
            var response = await _httpClient.GetAsync("/users");
            var result = await response.GetContentAs<dynamic>();
            Assert.NotNull(result);
        }
    }
}
