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
               Bio = "Drawgoon, is an environment and scenery artist who works for Blizzard Entertainment, and a member of the Sons of the Storm.He has made artwork for World of Warcraft: Chronicle Volume 1 and 2",
               AvatarUrl = "/images/avatar/Portrait_Drawgoon.png",
               UserId = "2738e787-5d57-4bc7-b0d2-287242f04695",
               ApprovedByAdmin = true,
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Glowei",
                Email = "glowei@aol.com",
                Bio = "Glowei is an artist who worked for Blizzard Entertainment and was the eighth \"Son\" of Sons of the Storm. He joined Blizzard after he met Mike Morhaime in China and showed him his portfolio.[1]\r\n\r\nWei Wang is most notably famous for having realized the majority of the most iconic artwork for World of Warcraft, such as expansion box cover art, race leader loading screens, and content patch artwork. He also created art for many World of Warcraft Trading Card Game cards, some of which have also been used for Hearthstone, and served as a cinematic artist for Hearthstone. He provided much concept art for the Warcraft film.[2] Wang left Blizzard Entertainment January 17, 2017.[3] He went on to found Imagendary Studios, a game development studio, in January 2021.",
                AvatarUrl = "/images/avatar/Portrait_Glowei.png",
                UserId = "0ac1e577-c7ff-4aa3-83c3-e5acac9de281",
                ApprovedByAdmin = true,
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Metzen",
                Email = "metzen@aol.com",
                Bio = "Metzen is the former Senior Vice President of Story & Franchise Development at Blizzard Entertainment.[1][2][3] He announced his retirement on September 12, 2016, citing a desire to spend more time with his family.",
                AvatarUrl = "/images/avatar/Portrait_Metzen.png",
                UserId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ApprovedByAdmin = true,
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "MrJack",
                Email = "mrjack@aol.com",
                Bio = "MrJack is a Concept Artist at Blizzard Entertainment.[1][2][3] He is also a member of the Sons of the Storm.",
                AvatarUrl = "/images/avatar/Portrait_MrJack.png",
                UserId = "f6f94be8-49e0-4a28-9e7f-797c40e7e169",
                ApprovedByAdmin = true,
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Raneman",
                Email = "raneman@aol.com",
                Bio = "Raneman was a Principal Artist on the World of Warcraft team at Blizzard Entertainment and Art Lead for Hearthstone. He is also the \"Fifth Son\" of Sons of the Storm. Having worked in the video game industry for over 15 years, his artwork and art direction represent all of Blizzard's games on magazines, book covers, game box covers, web banners, logos, concept art, merchandise, loading screens, at conventions, and in other high profile places.",
                AvatarUrl = "/images/avatar/Portrait_Raneman.png",
                UserId = "b4accad4-e878-4de3-a317-665d0a43fbd3",
                ApprovedByAdmin = true,
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "RedKnuckle",
                Email = "redknuckle@aol.com",
                Bio = "RedKnuckle is a former Blizzard employee and the seventh \"Son\" of Sons of the Storm. He was the lead concept artist for World of Warcraft before leaving to work with Riot Games and later departing into freelance work. He has also done artwork for Games Workshop.",
                AvatarUrl = "/images/avatar/Portrait_RedKnuckle.png",
                UserId = "5823bbf1-993c-416b-9bf1-c358fedf38a6",
                ApprovedByAdmin = true,
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Samwise",
                Email = "samwise@aol.com",
                Bio = "Samwise was a senior art director at Blizzard Entertainment.[1] He was one of Blizzard's first artists, joining the company in 1991.[2] Didier's greatly exaggerated physiques and vibrant, bright color palette dictated the artistic style of Warcraft III, World of Warcraft, StarCraft: Ghost and Heroes of the Storm. He has provided art direction or additional art on almost every Blizzard game released to date.",
                AvatarUrl = "/images/avatar/Portrait_Samwise.png",
                UserId = "ad8dada2-c947-4ad3-aaa1-e530f13d21c1",
                ApprovedByAdmin = true,
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Thammer",
                Email = "thammer@aol.com",
                Bio = "Thammer is one of Blizzard's most influential artists.Thammer worked on artwork featured in the Warcraft II manual,[1] and was responsible for much of the design of the undead, the goblins, and night elf structures in Warcraft III",
                AvatarUrl = "/images/avatar/Portrait_Thammer.png",
                UserId = "799d728e-0c16-4e4a-81b3-48a113a88cf1",
                ApprovedByAdmin = true,
            });

            await dbContext.Artists.AddAsync(new Artist
            {
                Nickname = "Twincruiser",
                Email = "twincruiser@aol.com",
                Bio = "Twincruiser, consisting of twin brothers are two of Blizzard Entertainment's most influential artists. During the World of Warcraft alpha, one of their first characters was an orc warrior.[1] The character was eventually enshrined in the game",
                AvatarUrl = "/images/avatar/Portrait_Twincruiser.png",
                UserId = "eb49ba9d-5030-42b6-8aef-c93506943fde",
                ApprovedByAdmin = true,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
