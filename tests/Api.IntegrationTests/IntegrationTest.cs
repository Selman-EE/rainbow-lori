using Application.Common;
using Application.Model.Request;
using Application.Model.Response;
using Domain.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Api.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(ApplicationDbContext));
                        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDb"));
                    });
                });

            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();

            _configuration = builder.Build();
            //
            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
            TestClient.DefaultRequestHeaders.Clear();
            TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task AuthenticateAsync(string token)
        {            
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            TestClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetSection("AllKeys:ApiKey")?.Value ?? string.Empty);
        }

        protected async Task<UserRes> UserRegisterAsync(UserReq request)
        {
            var response = await TestClient.PostAsJsonAsync("api/register", request);
            var result = await response.Content.ReadAsAsync<ResultExtended<UserRes>>();
            return result.Succeeded ? result.Data : null;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }
    }
}
