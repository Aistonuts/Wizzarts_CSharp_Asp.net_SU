//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Wizzarts.Common;
//using Wizzarts.Data;
//using Wizzarts.Data.Models;

//namespace Wizzarts.Services.Data.Tests.Mock.TestData
//{
//    public class RoleSeederTest : ITestDbSeeder
//    {
//        public async Task SeedAsync(ApplicationDbContext dbContext)
//        {
//            var services = new ServiceCollection();
//            var provider = services.BuildServiceProvider();
//            var roleManager = provider.GetRequiredService<RoleManager<ApplicationRole>>();

//            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName, GlobalConstants.AdministratorRoleGuid);
//            await SeedRoleAsync(roleManager, GlobalConstants.WizzartsTeamRoleName, GlobalConstants.WizzartsTeamRoleGuid);
//            await SeedRoleAsync(roleManager, GlobalConstants.MemberRoleName, GlobalConstants.MemberRoleGuid);
//            await SeedRoleAsync(roleManager, GlobalConstants.ArtistRoleName, GlobalConstants.ArtistRoleGuid);
//            await SeedRoleAsync(roleManager, GlobalConstants.StoreOwnerRoleName, GlobalConstants.StoreOwnerRoleGuid);
//            await SeedRoleAsync(roleManager, GlobalConstants.ContentCreatorRoleName, GlobalConstants.ContentCreatorRoleGuid);
//        }

//        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName, string id)
//        {
//            var role = await roleManager.FindByNameAsync(roleName);
//            if (role == null)
//            {
//                var result = await roleManager.CreateAsync(new ApplicationRole
//                {
//                    Id = id,
//                    Name = roleName,
//                    NormalizedName = roleName.ToUpper(),
//                });

//                if (!result.Succeeded)
//                {
//                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
//                }
//            }
//        }
//    }
//}
