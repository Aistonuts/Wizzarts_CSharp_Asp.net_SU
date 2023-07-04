namespace MagicCardsHub
{
    using MagicCardsHub.Data;
    using MagicCardsHub.Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CardsHubDbContext>(options =>
                options
                .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ProjectCreator, CreatorRole>()
                .AddEntityFrameworkStores<CardsHubDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
            });

            services.AddMvc().AddDataAnnotationsLocalization();
        }

        public void Configure(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<CardsHubDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.Roles.Any())
                    {
                        context.Roles.Add(new CreatorRole { Name = "Admin", NormalizedName = "ADMIN" });
                        context.Roles.Add(new CreatorRole { Name = "User", NormalizedName = "USER" });
                        context.Roles.Add(new CreatorRole { Name = "Artist", NormalizedName = "ARTIST" });
                    }

                  //
                  // if (!context.PackageStatus.Any())
                  // {
                  //     context.PackageStatus.Add(new PackageStatus { Name = "Pending" });
                  //     context.PackageStatus.Add(new PackageStatus { Name = "Shipped" });
                  //     context.PackageStatus.Add(new PackageStatus { Name = "Delivered" });
                  //     context.PackageStatus.Add(new PackageStatus { Name = "Acquired" });
                  // }

                    context.SaveChanges();
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {             
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
