using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wizzarts.Data.Models;

namespace Wizzarts.Data.Seeding
{
    public class EventCategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.EventCategories.Any())
            {
                return;
            }

            await dbContext.EventCategories.AddAsync(new EventCategory
            {
                Title = "Flavorless",
            });

            await dbContext.EventCategories.AddAsync(new EventCategory
            {
                Title = "Imageless",
            });

            await dbContext.EventCategories.AddAsync(new EventCategory
            {
                Title = "Redirect",
            });

            await dbContext.EventCategories.AddAsync(new EventCategory
            {
                Title = "Image",
            });

            await dbContext.EventCategories.AddAsync(new EventCategory
            {
                Title = "Text",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
