namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class ArticleSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.Articles.Any())
            {
                return;
            }

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Wizzarts card game release announcement.",
                ShortDescription = "Wizzarts website is the place where you can find all about our card game.",
                Description = "Wizzarts card game alpha deck  has been just released.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/MagicTheGathering.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The Wizzarts team.",
                ShortDescription = "Our team consist of artists and store owners.",
                Description = "We share common interest for art, tabletop games and collectible card games.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/SonsOfTheStorm.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The long wait is over.",
                ShortDescription = "Wizzarts card game alpha deck is already out.",
                Description = "Two months have passed since the announcement of our card game," +
                "se we  decided to release early our alpha deck." +
                "You can find our alpha deck in the stores.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/AlphaBox.png",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Alpha deck release events",
                ShortDescription = "We will need your help with completing the alpha deck",
                Description = "We were caught by surprise by the interest in our game and we were forced to release it early which came with a cost, our deck is incomplete." +
                "We will need your help with completing the alpha deck. We have added events for everyone interested in helping Wizzarts community." +
                "The Flavorless cards event is for our members but the Powerless cards event is only for artist, content creators and Wizzart team members." +
                "We will pick the best cards and we will add them to our deck.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/Champion.png",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Arena masters",
                ShortDescription = "Every battle needs a battleground.",
                Description = "If you are a store owner, passionate about board games and interested in hosting tournaments ,this is your chance. Click on become store owner," +
                "submit a picture of your store and fill your address, phone number and exact location.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/Arena.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Call to arms",
                ShortDescription = "We are representing the current open positions. Artist, content creator, arena master.",
                Description = "Check our membership page for more information. Store owners are always welcome. Members who participate in our events have better chances in joining our team." +
                "For those who are not participating in our events, check the membership web page to find out more.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/WrenchMan.jpg",
                ApprovedByAdmin = true,
                ForMainPage = true,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
