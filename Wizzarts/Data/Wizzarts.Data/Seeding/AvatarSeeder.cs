namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

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

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Drawgoon",
                AvatarUrl = "/images/avatar/Portrait_Drawgoon.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Glowei",
                AvatarUrl = "/images/avatar/Portrait_Glowei.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Metzen",
                AvatarUrl = "/images/avatar/Portrait_Metzen.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "MrJack",
                AvatarUrl = "/images/avatar/Portrait_MrJack.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Raneman",
                AvatarUrl = "/images/avatar/Portrait_Raneman.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "RedKnuckle",
                AvatarUrl = "/images/avatar/Portrait_RedKnuckle.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Samwise",
                AvatarUrl = "/images/avatar/Portrait_Samwise.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Thammer",
                AvatarUrl = "/images/avatar/Portrait_Thammer.png",
            });

            await dbContext.Avatars.AddAsync(new Avatar
            {
                Name = "Twincruiser",
                AvatarUrl = "/images/avatar/Portrait_Twincruiser.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
