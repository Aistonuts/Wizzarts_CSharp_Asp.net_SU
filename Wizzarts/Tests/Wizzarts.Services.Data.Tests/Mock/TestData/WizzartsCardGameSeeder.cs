namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class WizzartsCardGameSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.WizzartsCardGame.Any())
            {
                return;
            }

            await dbContext.WizzartsCardGame.AddAsync(new WizzartsCardGame
            {
                Id = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
                Title = "Wizzarts",
                Description = "Collection card game",
                CardGameRulesId = 1,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
