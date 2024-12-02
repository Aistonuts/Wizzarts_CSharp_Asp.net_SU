namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class EventStatusSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.EventStatuses.Any())
            {
                return;
            }

            await dbContext.EventStatuses.AddAsync(new EventStatus
            {
                Name = "Ongoing",
            });

            await dbContext.EventStatuses.AddAsync(new EventStatus
            {
                Name = "Completed",
            });

            await dbContext.EventStatuses.AddAsync(new EventStatus
            {
                Name = "Closed",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
