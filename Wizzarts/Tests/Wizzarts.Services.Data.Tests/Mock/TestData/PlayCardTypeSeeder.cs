namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class PlayCardTypeSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.PlayCardTypes.Any())
            {
                return;
            }

            await dbContext.PlayCardTypes.AddAsync(new PlayCardType
            {
                Name = "Not Defined Yet!!!",
            });

            await dbContext.PlayCardTypes.AddAsync(new PlayCardType
            {
                Name = "Artifact",
            });

            await dbContext.PlayCardTypes.AddAsync(new PlayCardType
            {
                Name = "Enchantment",
            });

            await dbContext.PlayCardTypes.AddAsync(new PlayCardType
            {
                Name = "Land",
            });

            await dbContext.PlayCardTypes.AddAsync(new PlayCardType
            {
                Name = "Creature",
            });

            await dbContext.PlayCardTypes.AddAsync(new PlayCardType
            {
                Name = "Instant",
            });

            await dbContext.PlayCardTypes.AddAsync(new PlayCardType
            {
                Name = "Sorcery",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
