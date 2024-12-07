namespace Wizzarts.Services.Data.Tests.Mock
{
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class CardGameExpansionSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.CardGameExpansions.Any())
            {
                return;
            }

            await dbContext.CardGameExpansions.AddAsync(new CardGameExpansion
            {
                Title = "Alpha",
                Description = "Core Game",
                CardsCount = "10",
                ExpansionSymbolUrl = "/images/symbols/expansions/Alpha.png",
            });
            await dbContext.CardGameExpansions.AddAsync(new CardGameExpansion
            {
                Title = "Second",
                Description = "The expansion where the name of card and its abilities were added during the Nameless Cards Event",
                CardsCount = "13",
                ExpansionSymbolUrl = "/images/symbols/expansions/Beta.png",
            });
            await dbContext.CardGameExpansions.AddAsync(new CardGameExpansion
            {
                Title = "Beta",
                Description = "The expansion where the name of card and its abilities were added during the Faceless Cards Event",
                CardsCount = "31",
                ExpansionSymbolUrl = "/images/symbols/expansions/Unlimited.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
