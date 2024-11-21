namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class DeckSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CardDecks.Any())
            {
                return;
            }

            await dbContext.CardDecks.AddAsync(new CardDeck
            {
                Name = "Dark rituals",
                Description = "Using summons, sorcery and black mana",
                ShippingAddress = "Can be shipped to local stores",
                ImageUrl = "/images/art/Alpha/Ancestral_Recall.png",
                StoreId = 1,
                StatusId = 2,
                CreatedByMemberId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                IsLocked = true,
            });

            await dbContext.CardDecks.AddAsync(new CardDeck
            {
                Name = "Heaven and Hell",
                Description = "The enchantment deck",
                ShippingAddress = "Can be shipped to local stores",
                ImageUrl = "/images/art/Alpha/Bad_Moon.png",
                StoreId = 1,
                StatusId = 2,
                CreatedByMemberId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                IsLocked = true,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
