namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class DeckOfCardsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.DeckOfCards.Any())
            {
                return;
            }

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "c330fecf-61a9-4e03-8052-cd2b9583a251",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "c330fecf-61a9-4e03-8052-cd2b9583a251",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "4ece25e1-5d3a-4109-8d92-74864fc7da8a",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "4ece25e1-5d3a-4109-8d92-74864fc7da8a",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "91c884eb-ca3a-4571-8eee-341eba48029b",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "632d8f1f-4fdf-4a28-a0b5-6ae083e91580",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "632d8f1f-4fdf-4a28-a0b5-6ae083e91580",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "632d8f1f-4fdf-4a28-a0b5-6ae083e91580",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "ea971255-f368-4fa5-adb9-ddf18f58fc6f",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "ea971255-f368-4fa5-adb9-ddf18f58fc6f",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "5f3f96a8-836a-479c-93c8-6921feb79366",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "5f3f96a8-836a-479c-93c8-6921feb79366",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "9c2f3f79-cef6-4dec-9b55-a55d54b5fb7f",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "9c2f3f79-cef6-4dec-9b55-a55d54b5fb7f",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "29d331a8-b9a7-4932-adff-6d6dc1c57d9c",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "29d331a8-b9a7-4932-adff-6d6dc1c57d9c",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "29d331a8-b9a7-4932-adff-6d6dc1c57d9c",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "6c0b467f-ce6a-4d90-9ec7-5c3bb8077cca",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 1,
                PlayCardId = "6c0b467f-ce6a-4d90-9ec7-5c3bb8077cca",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "940c79ba-4aeb-4abe-bdf5-bb639144c306",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "940c79ba-4aeb-4abe-bdf5-bb639144c306",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "940c79ba-4aeb-4abe-bdf5-bb639144c306",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "5f3f96a8-836a-479c-93c8-6921feb79366",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "5f3f96a8-836a-479c-93c8-6921feb79366",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "5f3f96a8-836a-479c-93c8-6921feb79366",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "4160af89-6994-4f5f-8a37-2df3b85c825c",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "4160af89-6994-4f5f-8a37-2df3b85c825c",
            });

            await dbContext.DeckOfCards.AddAsync(new DeckOfCards
            {
                DeckId = 2,
                PlayCardId = "4160af89-6994-4f5f-8a37-2df3b85c825c",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
