using System.Linq;
using System.Threading.Tasks;
using Wizzarts.Data;
using Wizzarts.Data.Models;

namespace Wizzarts.Services.Data.Tests.Mock.TestData
{
    public class DeckSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.CardDecks.Any())
            {
                return;
            }

            await dbContext.CardDecks.AddAsync(new CardDeck()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                ShippingAddress = "Test",
                StatusId = 1,
                StoreId = 1,
                CreatedByMemberId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
