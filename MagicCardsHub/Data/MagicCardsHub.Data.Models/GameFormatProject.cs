namespace MagicCardsHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsHub.Data.Common.Models;

    public class GameFormatProject : BaseDeletableModel<int>
    {
        public GameFormatProject()
        {
            this.Cards = new HashSet<PlayCard>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfCards { get; set; }

        public string ProjectStatusId { get; set; }

        public virtual ProjectStatus ProjectStatus { get; set; }

        public string ProjectCreatorId { get; set; }

        public virtual ApplicationUser ProjectCreator { get; set; }

        public virtual ICollection<PlayCard> Cards { get; init; }
    }
}
