namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class ArtistSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Artists.Any())
            {
                return;
            }

            await dbContext.Artists.AddAsync(new Artist
            {
               Nickname = "Drawgoon",
               Email = "drawgoon@aol.com",
               AvatarUrl = "/images/avatar/Portrait_Dawgoon.png",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Glowei",
                Email = "glowei@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Glowei.png",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Metzen",
                Email = "metzen@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Metzen.png",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "MrJack",
                Email = "mrjack@aol.com",
                AvatarUrl = "/images/avatar/Portrait_MrJack.png"
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Raneman",
                Email = "raneman@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Raneman.png",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "RedKnuckle",
                Email = "redknuckle@aol.com",
                AvatarUrl = "/images/avatar/Portrait_RedKnuckle.png"
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Samwise",
                Email = "samwise@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Samwise.png",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Thammer",
                Email = "thammer@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Thammer.png",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Twincruiser",
                Email = "twincruiser@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Twincruiser.png",
            });
        }
    }
}
