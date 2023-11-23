namespace MagicCardsHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Models;

    public class GameExpansionSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.GameExpansions.Any())
            {
                return;
            }

            await dbContext.GameExpansions.AddAsync(new GameExpansion { Title = "Alpha" , Description = "Our first game expansion.", CardsCount = "3", RemoteImageUrl = "/symbols/expansions/Alpha.png" });
            await dbContext.GameExpansions.AddAsync(new GameExpansion { Title = "Beta" , Description = "Our second expansion.", CardsCount = "4", RemoteImageUrl = "/symbols/expansions/Beta.png" });
            await dbContext.GameExpansions.AddAsync(new GameExpansion { Title = "Unlimited", Description = "Revised cards from Alpha and Beta, also many new cards", CardsCount = "10", RemoteImageUrl = "/symbols/expansions/Unlimited.png" });

            await dbContext.SaveChangesAsync();
        }
    }
}
