namespace Wizzarts.Services.Data.Tests.Mock.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class ManaCostSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.ManaCosts.Any())
            {
                return;
            }

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 0,
                Color = "Colorless",
                RemoteImageUrl = string.Empty,
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 1,
                Color = "Colorless",
                RemoteImageUrl = "/images/mana/1.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 2,
                Color = "Colorless",
                RemoteImageUrl = "/images/mana/2.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 3,
                Color = "Colorless",
                RemoteImageUrl = "/images/mana/3.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 4,
                Color = "Colorless",
                RemoteImageUrl = "/images/mana/4.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 5,
                Color = "Colorless",
                RemoteImageUrl = "/images/mana/5.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 0,
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 1,
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 2,
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 3,
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 4,
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 5,
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 0,
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 1,
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 2,
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 3,
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 4,
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 5,
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 0,
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 1,
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 2,
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 3,
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 4,
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 5,
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 0,
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 1,
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 2,
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 3,
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 4,
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 5,
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
            });


            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 0,
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 1,
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 2,
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 3,
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 4,
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
            });

            await dbContext.ManaCosts.AddAsync(new ManaCost
            {
                Cost = 5,
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
