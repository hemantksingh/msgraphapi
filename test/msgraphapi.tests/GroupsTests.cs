using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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
        public async Task GetAllGroups()
        {
            var response = await _httpClient.GetAsync("/groups");
            var result = await response.GetContentAs<dynamic>();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUsersReturnsOnlyUsersInAGroup()
        {
            var response = await _httpClient.GetAsync("/groups/3164bf02-2ba3-420f-bbb8-4d6e1b9ac945/users");
            var result = await response.GetContentAs<IEnumerable<User>>();
            Assert.NotNull(result);
            Assert.Contains(result, x => x.type == "#microsoft.graph.user");
        }
    }
}
