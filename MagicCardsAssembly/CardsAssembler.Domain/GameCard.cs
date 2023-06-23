using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAssembler.Domain
{
    public class GameCard
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();        
    
        public string ProjectId { get; set; }

        public GameProjectCard Project { get; set; }

        public string StatusId { get; set; }

        public CardStatus Status { get; set; }

        public string ArtId { get; set; }

        public Art Art { get; set; }

        public string CardStatsId { get; set; }

        public CardStats CardStats { get; set; }
    }
}
