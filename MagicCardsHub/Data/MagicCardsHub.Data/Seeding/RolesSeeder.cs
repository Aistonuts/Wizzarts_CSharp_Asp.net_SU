namespace MagicCardsHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Common;
    using MagicCardsHub.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.UserRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.ArtistRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.StoreOwnerRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.ContentCreatorRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
