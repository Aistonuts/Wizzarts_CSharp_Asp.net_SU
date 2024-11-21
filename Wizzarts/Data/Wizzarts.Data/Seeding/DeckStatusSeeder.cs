namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class DeckStatusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.DeckStatuses.Any())
            {
                return;
            }

            await dbContext.DeckStatuses.AddAsync(new DeckStatus
            {
                Name = "Open",
            });
            await dbContext.DeckStatuses.AddAsync(new DeckStatus
            {
                Name = "Ready",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
