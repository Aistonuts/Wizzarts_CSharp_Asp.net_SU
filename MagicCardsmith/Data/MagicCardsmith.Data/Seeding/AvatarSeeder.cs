namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class AvatarSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Avatars.Any())
            {
                return;
            }

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Marvel One",
                AvatarUrl = "/images/avatar/Marvel_One.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Marvel Two",
                AvatarUrl = "/images/avatar/Marvel_Two.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Marvel Three",
                AvatarUrl = "/images/avatar/Marvel_Three.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Marvel Four",
                AvatarUrl = "/images/avatar/Marvel_Four.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Marvel Five",
                AvatarUrl = "/images/avatar/Marvel_Five.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Marvel Six",
                AvatarUrl = "/images/avatar/Marvel_Six.png",
            });
        }
    }
}
