using MagicCardsmith.Data.Models;

namespace MagicCardsmith.Data.Seeding
{
    using MagicCardsmith.Services.Data.Tests.SeedTestDb;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class TestArticleSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {

            if (dbContext.Articles.Any())
            {
                return;
            }

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The Magic Cardsmith platform has arrived.",
                ShortDescription = "The Magic cardsmith is a platform for creating tabletop and digita collectible card games.",
                Description = "Our current project is the Alpha Magic cardsmith. The Alpha deck has been just released. It is already avaialble in our stores.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/MagicTheGathering.jpg",
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The Magic Cardsmith team.",
                ShortDescription = "Our team consist of artists, playgame testers, card flavour creators and store owners.",
                Description = "Common interest for art tabletob games and collectible card games was the main reason for creating this platform were we can exchange our ideas with anyone interested.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/SonsOfTheStorm.jpg",
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The long wait is over.Beta deck is here",
                ShortDescription = "Two months have passed since the release of our Aplha deck and it was a blast.",
                Description = "Since the release of our alpha deck we noticed your huge interest so we decided to release our Beta deck in the upcoming month," +
                "however we need your help.Before releasing the Beta deck and the next Unlimited deck we will give the chance to few of you to partially participate in creating the Beta deck and later" +
                "join our team for assisting us with creating the Unlimited deck. For more information check the events board.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/AlphaBox.png",
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Beta deck pre-release events",
                ShortDescription = "We are representing our first events board.The Magic Cardsmith community needs your help for releasing the upcomming expansion as soon as possible.",
                Description = "Our Beta deck is complete, but there is still place for your ideas and we will gladly replace some of our cards with better ones created by you." +
                "Few of you will have the chance to share their ideas with us .Thanks to our vote system, " +
                "we will pick those who are worthy enough to join our team. Every card needs a certain type of flavour and an art of course, the art is what gives our cards they magical appearance" +
                "but the flavour grants its powers." +
                "For those participating in the events a chance to join our team as an artist, game play mechanics tester or admin will be offered." +
                "All you have to do is to participate in the vent, complete the milestones and  get enough votes from our community and we will pick those of you with the higher number of votes.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/Champion.png",
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Arena masters",
                ShortDescription = "Every battle needs a suitable battleground.",
                Description = "If you are a store owner, passionate about board games and interested in hosting tournaments ,this is your chance. Click on become store owner," +
                "submit a picture of your store and fill your address, phone number and exact location.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/Arena.jpg",
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Call to arms",
                ShortDescription = "We are representing the current open positions. Artist, card flavour creator, arena master and game tester.",
                Description = "Do you see yourself as an artist? To find out, participate in our events and espacially those made for artists, where the card flavour and power is provided but there is noone worthy enough to wield them." +
                "Maybe you are a store owner interested in hosting tournaments ? Participating in our events where a new play mechanic need testing will be the best place to start.Each game needs an arena and each arena its Arena master." +
                "For those interested in testing their new play cards before submitting it we have provided a specia tool called Cardsmither which will allow you to forge your card and test it along with your other cards." +
                "Becoming a card flavour creator or a game tester is not an easy job, it needs teamwork. Teaming up is allowed.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/WrenchMan.jpg",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
