using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wizzarts.Data;
namespace Wizzarts.Web
{
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Wizzarts.Data;
    using Wizzarts.Data.Common;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Data.Seeding;
    using Wizzarts.Services.Data;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Services.Messaging;
    using Wizzarts.Web.SignalRChatApp.Hubs;
    using Wizzarts.Web.ViewModels;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddMemoryCache();
            services.AddSignalR();
            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddSingleton(configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArtService, ArtService>();
            services.AddTransient<IPlayCardService, PlayCardService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IPlayCardExpansionService, PlayCardExpansionService>();
            services.AddTransient<IPlayCardComponentsService, PlayCardComponentsService>();
            services.AddTransient<IWizzartsServices, WizzartsServices>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IChatService, ChatService>();
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (dbContext.Database.IsRelational())
                {
                    dbContext.Database.Migrate();
                }
                else
                {
                    dbContext.Database.EnsureCreated();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<ChatHub>("/chat");
            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }
    }
}
