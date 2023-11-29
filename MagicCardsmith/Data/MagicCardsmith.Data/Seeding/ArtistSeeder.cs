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

            if (dbContext.Users.Any())
            {
                return;
            }

            await dbContext.Artists.AddAsync(new Artist
            {
               Nickname = "Drawgoon",
               Email = "drawgoon@aol.com",
               AvatarUrl = "/images/avatar/Portrait_Dawgoon.png",
               UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Glowei",
                Email = "glowei@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Glowei.png",
                UserId = "0ac1e577-c7ff-4aa3-83c3-e5acac9de281",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Metzen",
                Email = "metzen@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Metzen.png",
                UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "MrJack",
                Email = "mrjack@aol.com",
                AvatarUrl = "/images/avatar/Portrait_MrJack.png",
                UserId = "f6f94be8-49e0-4a28-9e7f-797c40e7e169",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Raneman",
                Email = "raneman@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Raneman.png",
                UserId = "b4accad4-e878-4de3-a317-665d0a43fbd3",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "RedKnuckle",
                Email = "redknuckle@aol.com",
                AvatarUrl = "/images/avatar/Portrait_RedKnuckle.png",
                UserId = "5823bbf1-993c-416b-9bf1-c358fedf38a6",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Samwise",
                Email = "samwise@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Samwise.png",
                UserId = "ad8dada2-c947-4ad3-aaa1-e530f13d21c1",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Thammer",
                Email = "thammer@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Thammer.png",
                UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Twincruiser",
                Email = "twincruiser@aol.com",
                AvatarUrl = "/images/avatar/Portrait_Twincruiser.png",
                UserId = "eb49ba9d-5030-42b6-8aef-c93506943fde",
            });
        }
    }
}
