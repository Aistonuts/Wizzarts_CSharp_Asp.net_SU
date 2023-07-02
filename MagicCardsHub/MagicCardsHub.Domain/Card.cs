using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsHub.Domain
{
    public class Card
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public int Health { get; set; }
        public string GameSeasonId { get; set; }
        public GameSeason GameSeason { get; set; }

    }
}
