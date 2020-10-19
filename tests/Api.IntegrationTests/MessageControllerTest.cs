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
    public class MessageControllerTest : IntegrationTest
    {
        [Fact]
        public async Task Create_Chat_Room_For_Talk()
        {
            // Arrange            
            var actualUserRegistered = await UserRegisterAsync(new UserReq
            {
                Name = "Test",
                Surname = "Test",
                EmailAddress = "seet3@gmail.com",
                Username = "see3",
                Password = "123123aB1"
            });
            await AuthenticateAsync(actualUserRegistered.AccessToken);
            var actual = await TestClient.GetAsync("api/start-chat");
            var response = await actual.Content.ReadAsAsync<ResultExtended<string>>();
            //
            // Act
            var messageReq = new MessageReq
            {
                Username = "see",
                Text = "Hello Jack!",
            };
            var expected = await TestClient.PostAsJsonAsync($"api/{response.Data}/send-message", messageReq);
            //
            // Assert
            expected.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await expected.Content.ReadAsAsync<ResultExtended<MessageRes>>();
            result.Data.ReceiverUsername.Should().Be(messageReq.Username);
            result.Data.Text.Should().Be(messageReq.Text);
        }
    }
}
