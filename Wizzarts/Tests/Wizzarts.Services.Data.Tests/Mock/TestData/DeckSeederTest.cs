namespace Wizzarts.Services.Data.Tests.Mock.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class DeckSeederTest : ITestDbSeeder

    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.CardDecks.Any())
            {
                return;
            }

            await dbContext.CardDecks.AddAsync(new CardDeck()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StatusId = 1,
                StoreId = 1,
                CreatedByMemberId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                IsLocked = false,
            });

            await dbContext.CardDecks.AddAsync(new CardDeck()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StatusId = 1,
                StoreId = 1,
                CreatedByMemberId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                IsLocked = false,
            });

            await dbContext.CardDecks.AddAsync(new CardDeck()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StatusId = 2,
                StoreId = 1,
                CreatedByMemberId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                IsLocked = true,
            });

            await dbContext.CardDecks.AddAsync(new CardDeck()
            {
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StatusId = 3,
                StoreId = 1,
                CreatedByMemberId = "0ac1e577-c7ff-4aa3-83c3-e5acac9de281",
                IsLocked = true,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}