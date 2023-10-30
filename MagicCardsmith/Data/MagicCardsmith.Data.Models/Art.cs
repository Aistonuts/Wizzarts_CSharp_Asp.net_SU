namespace MagicCardsmith.Data.Models
{
    using System;

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

        public string ArtIstId { get; set; }

        public ApplicationUser Artist { get; set; }
    }
}
