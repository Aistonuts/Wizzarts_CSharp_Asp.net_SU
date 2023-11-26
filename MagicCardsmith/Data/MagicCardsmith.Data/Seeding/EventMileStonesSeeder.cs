using MagicCardsmith.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MagicCardsmith.Data.Seeding
{
    public class EventMileStonesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.EventMilestones.Any())
            {
                return;
            }

            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Call to arms",
                Description = "",
                ImageUrl= " ",

            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Call to arms",
                Description = "",
                ImageUrl = " ",

            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Call to arms",
                Description = "",
                ImageUrl = " ",

            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Call to arms",
                Description = "",
                ImageUrl = " ",

            });
        }
    }
}