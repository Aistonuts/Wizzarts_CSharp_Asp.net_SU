using MagicCardsmith.Data.Models;

namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArticleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Articles.Any())
            {
                return;
            }

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Greetings players",
                ShortDescription= "Magic: The Gathering (colloquially known as Magic or MTG) is a tabletop and digital collectible card game created by Richard Garfield.[1]",
                Description = "Magic: The Gathering (colloquially known as Magic or MTG) is a tabletop and digital collectible card game created by Richard Garfield.[1] " +
                "Released in 1993 by Wizards of the Coast (now a subsidiary of Hasbro)," +
                " Magic was the first trading card game and had approximately thirty-five million players as of December 2018," +
                "[2][3][4] and over twenty billion Magic cards were produced in the period from 2008 to 2016, during which time it grew in popularity.[5][6]",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/MagicTheGathering.jpg",
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "Our team",
                ShortDescription = "Team made of artist in collaboration with Sons of the Storm.",
                Description = "The Sons of the Storm are Blizzard Entertainment artists who published their art on a common art website which are Samwise Didier, Chris Metzen, René Koiter (of Twincruiser), Thammer, Raneman, Drawgoon, Red Knuckle, Glowei, and Mr. Jack." +
                " Three affiliated non-Sons are Mickelob, Michel Koiter (of Twincruiser), and Sixen." +
                "Forsaking such rewarding pursuits as painting signs and cleaning crap-ridden movie theaters; no longer content to repair watches," +
                " park cars and serve summonses to the dregs of humanity; sick of sleeping in the back seats of Dodge Demons, Ford Mavericks and on friends' parents' patios;" +
                " weary of abusing one-month free trial memberships to shower at local gyms under the scrutinizing gaze of geriatric racquetball aficionados..." +
                " this motley pack of wanderers joined under the banner of the storm and united to form a mighty clan of artists, storytellers and world-creators." +
                " This is their domain where you will find a haven against such soul-leeching madness as cell phones, reality TV, MySpace, the Gilmore Girls, American Idol and Starbucks." +
                " Starbuck is a STUD fighter pilot from Battlestar Galactica, not a place that hawks seven dollar mochas. Cast off your shackles and answer the call of the storm!",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/SonsOfTheStorm.jpg",
            });

            await dbContext.Articles.AddAsync(new Article
            {
                Title = "The long wait is over.Alpha deck is here",
                ShortDescription = "Our alpha deck is here and that is not all.We have not included the full set of cards.",
                Description = "Our alpha deck is here and that is not all.We have not included the full set of cards." +
                "We have left some space for your Ideas.We have prepared two events for you , each with certain numbe of mile stones." +
                "We are prepared, we have the full set of cards ready but we decided to encourage our games creativity." +
                "Check the event page.Complete the milesstores.There might be a winner.It depends on how motivated you are.",
                ArticleCreatorId = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                ImageUrl = "/images/navigation/AlphaBox.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
