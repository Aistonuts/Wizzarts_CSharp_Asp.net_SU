namespace Wizzarts.Web.Tests
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using Wizzarts.Data.Models;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.ReplaceSingleton<SignInManager<ApplicationUser>, SignInManagerMock>();
        }
    }
}
