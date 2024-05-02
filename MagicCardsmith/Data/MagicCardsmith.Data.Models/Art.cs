namespace MagicCardsmith.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class Art : BaseDeletableModel<string>
    {
        public Art()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }

        public int? ArtIstId { get; set; }

        public Artist Artist { get; set; }

        public int? CardId { get; set; }

        public Card Card { get; set; }

        public bool IsEventArt { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
