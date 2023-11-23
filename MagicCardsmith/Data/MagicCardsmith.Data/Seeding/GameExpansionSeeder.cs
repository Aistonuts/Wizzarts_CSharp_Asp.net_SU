namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class GameExpansionSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.GameExpansions.Any())
            {
                return;
            }

            await dbContext.GameExpansions.AddAsync(new GameExpansion
            {
              Title = "Alpha",
              Description = "Our first game expansion.",
              CardsCount = "23",
              ExpansionSymbolUrl = "images/symbols/expansions/Alpha.png",
            });
            await dbContext.GameExpansions.AddAsync(new GameExpansion
            {
             Title = "Beta",
             Description = "Our second expansion.Announcing new challenge",
             CardsCount = "10",
             ExpansionSymbolUrl = "images/symbols/expansions/Beta.png",
            });
            await dbContext.GameExpansions.AddAsync(new GameExpansion 
            {
             Title = "Unlimited",
             Description = "Revised cards from Alpha and Beta, also many new cards",
             CardsCount = "33",
             ExpansionSymbolUrl = "images/symbols/expansions/Unlimited.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
