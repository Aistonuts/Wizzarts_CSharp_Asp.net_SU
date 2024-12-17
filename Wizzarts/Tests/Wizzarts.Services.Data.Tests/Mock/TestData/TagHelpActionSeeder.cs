namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class TagHelpActionSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.TagHelpActions.Any())
            {
                return;
            }

            await dbContext.TagHelpActions.AddAsync(new TagHelpAction
            {
                Id = "a1e33d52-660d-4cc9-b6b9-c04ba4b9ec70",
                Name = "Create",
            });

            await dbContext.TagHelpActions.AddAsync(new TagHelpAction
            {
                Id = "cf3494ef-93cc-42d2-bf8d-fc733adc3973",
                Name = "ById",
            });

            await dbContext.TagHelpActions.AddAsync(new TagHelpAction
            {
                Id = "22011e99-c794-4fc3-b277-fb446b253d4c",
                Name = "Add",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
