using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace msgraphapi.tests
{
    public class GroupsTests
    {
        private readonly HttpClient _httpClient;

        public GroupsTests()
        {
            _httpClient = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()).CreateClient();
        }

        [Fact]
        public async Task GetUsersInAGroup()
        {
            var response = await _httpClient.GetAsync("/groups/3164bf02-2ba3-420f-bbb8-4d6e1b9ac945/users");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(stringResponse);
            Assert.NotNull(result);
        }
    }
}
