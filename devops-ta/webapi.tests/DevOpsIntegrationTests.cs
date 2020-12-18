using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using webapi.Models;

namespace webapi.tests
{
    public class DevOpsIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public DevOpsIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_DevOpsEndpointReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();
            DevOpsRequest model = new DevOpsRequest()
            {
                Message = "This is a test",
                To = "Juan Perez",
                From = "Rita Asturia",
                TimeToLifeSec = 45

            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/DevOps", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.True(response.Headers.Contains("X-JWT-KWY"));
        }
    }
}
