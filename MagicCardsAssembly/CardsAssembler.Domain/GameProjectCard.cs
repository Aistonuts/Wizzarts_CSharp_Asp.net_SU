using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CardsAssembler.Domain
{
    public class GameProjectCard
    {
        public GameProjectCard()
        {
            this.GameCard = new List<GameCard>();
        }
        public int Id { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }      
        public ICollection<GameCard> GameCard { get; init; } 
    }
}
