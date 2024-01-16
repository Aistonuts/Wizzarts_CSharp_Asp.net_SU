namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class CardTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CardTypes.Any())
            {
                return;
            }

            await dbContext.CardTypes.AddAsync(new CardType
            {
                Name = "Artifact",
            });

            await dbContext.CardTypes.AddAsync(new CardType
            {
                Name = "Enchantment",
            });

            await dbContext.CardTypes.AddAsync(new CardType
            {
                Name = "Land",
            });

            await dbContext.CardTypes.AddAsync(new CardType
            {
                Name = "Creature",
            });

            await dbContext.CardTypes.AddAsync(new CardType
            {
                Name = "Instant",
            });

            await dbContext.CardTypes.AddAsync(new CardType
            {
                Name = "Sorcery",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
