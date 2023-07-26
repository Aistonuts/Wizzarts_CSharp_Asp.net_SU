namespace MagicCardsHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Models;

    internal class PackageStatusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PackageStatuses.Any())
            {
                return;
            }

            await dbContext.PackageStatuses.AddAsync(new PackageStatus { Name = "Pending" });
            await dbContext.PackageStatuses.AddAsync(new PackageStatus { Name = "Shipped" });
            await dbContext.PackageStatuses.AddAsync(new PackageStatus { Name = "Delivered" });
            await dbContext.PackageStatuses.AddAsync(new PackageStatus { Name = "Acquired" });
        }
    }
}
