using Application.Common;
using Application.Model.Request;
using Application.Model.Response;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    public class IdentityControllerTest : IntegrationTest
    {
        [Fact]
        public async Task New_User_Register()
        {
            // Arrange            
            var actual = await UserRegisterAsync(new UserReq
            {
                Name = "Test",
                Surname = "Test",
                EmailAddress = "seet3@gmail.com",
                Username = "see3",
                Password = "123123aB1"
            });

            // Act
            var response = await TestClient.PostAsJsonAsync("api/login", new LoginReq
            {
                Username = "see3",
                Password = "123123aB1"
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsAsync<ResultExtended<LoginRes>>();
            result.Data.AccessToken.Should().NotBeNullOrEmpty();            
        }
    }
}
