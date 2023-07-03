using MagicCardsHub.Domain;

namespace MagicCardsHub.Models.Card
{
    public class CardCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public int Health { get; set; }      
        public string GameSeason { get; set; }
    }
}
