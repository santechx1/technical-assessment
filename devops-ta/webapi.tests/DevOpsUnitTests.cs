using webapi.Controllers;
using webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace webapi.tests
{
    public class DevOpsUnitTests
    {
        [Fact]
        public void HttpPost_Request_Returns_DevOpsResponse()
        {
            //Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(m => m[It.Is<string>(s => s == "JwtSecret")]).Returns("Dev0000000psRule$$$$$$$$$$$$$$$$$$$");
            DevOpsRequest request = new DevOpsRequest()
            {
                Message = "This is a test",
                To = "Juan Perez",
                From = "Rita Asturia",
                TimeToLifeSec = 45

            };
            string spectedResponseMessage = $"Hello {request.To} your message will be send";

            //Act
            var controller = new DevOpsController(configMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            var response = controller.Post(request);

            //Assert
            Assert.IsType<DevOpsResponse>(response);
            Assert.Equal(spectedResponseMessage, response.Message);
        }
    }
}
