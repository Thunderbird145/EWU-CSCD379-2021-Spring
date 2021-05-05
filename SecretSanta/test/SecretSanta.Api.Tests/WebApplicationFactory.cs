using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SecretSanta.Business;
using SecretSanta.Api.Tests.Business;
using Microsoft.Extensions.DependencyInjection;

namespace SecretSanta.Api.Tests
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {

        public TestableUserRepository Repository {get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                services.AddScoped<IUserRepository, TestableUserRepository>(_ => Repository);
            });
        }
    }
}