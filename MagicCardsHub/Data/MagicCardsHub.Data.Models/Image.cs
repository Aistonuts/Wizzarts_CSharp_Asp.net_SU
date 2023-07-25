namespace MagicCardsHub.Data.Models
{
    using MagicCardsHub.Data.Common.Models;
    using System;
    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        //// The contents of the image is in the file system

        public int? ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public string DigitalArtId { get; set; }

        public virtual DigitalArt DigitalArt { get; set; }

        public int? StoreImageId { get; set; }

        public virtual Store StoreImage { get; set; }

        public int ProjectIdImage { get; set; }

        public virtual GameFormatProject ProjectImage { get; set; }

        public int? TournamentImageId { get; set; }

        public virtual StoreTournament TournamentImage { get; set; }

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
