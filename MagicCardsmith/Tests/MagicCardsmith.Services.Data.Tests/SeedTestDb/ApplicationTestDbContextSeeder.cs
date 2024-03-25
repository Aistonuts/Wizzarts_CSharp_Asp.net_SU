namespace MagicCardsmith.Services.Data.Tests.SeedTestDb
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Seeding;

    public class ApplicationTestDbContextSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            var seeders = new List<ITestDbSeeder>
                          {
                              new TestArticleSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
