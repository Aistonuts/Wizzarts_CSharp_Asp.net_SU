namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class DeckStatusSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
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

            await dbContext.DeckStatuses.AddAsync(new DeckStatus
            {
                Name = "Shipped",
            });

            await dbContext.DeckStatuses.AddAsync(new DeckStatus
            {
                Name = "Delivered",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

