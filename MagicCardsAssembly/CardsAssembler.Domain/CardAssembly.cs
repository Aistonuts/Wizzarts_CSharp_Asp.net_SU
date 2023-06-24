using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CardsAssembler.Domain
{
    public class CardAssembly
    {
        public CardAssembly()
        {
            this.GameCard = new List<Card>();
        }
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectManagerId { get; set; }
        public CardCreator ProjectManager { get; set; }
        public ICollection<Card> GameCard { get; init; } 
    }
}
