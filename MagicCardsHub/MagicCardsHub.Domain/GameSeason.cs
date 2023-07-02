using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsHub.Domain
{
    public class GameSeason
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectStatusId { get; set; }

        public ProjectStatus ProjectStatus { get; set; }

        public string ProjectCreatorId { get; set; }
        public ProjectCreator ProjectCreator { get; set; }
        public ICollection<Card> Cards { get; init; }
    }
}
