namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class WizzartsCardGameSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
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
