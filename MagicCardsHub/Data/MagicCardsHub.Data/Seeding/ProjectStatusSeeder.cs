namespace MagicCardsHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Models;

    internal class ProjectStatusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ProjectStatuses.Any())
            {
                return;
            }

            await dbContext.ProjectStatuses.AddAsync(new ProjectStatus { Name = "Open" });
            await dbContext.ProjectStatuses.AddAsync(new ProjectStatus { Name = "Work in progress" });
            await dbContext.ProjectStatuses.AddAsync(new ProjectStatus { Name = "Pending" });
            await dbContext.ProjectStatuses.AddAsync(new ProjectStatus { Name = "Fulfilled" });
        }
    }
}
