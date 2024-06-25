namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class EventStatusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
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
