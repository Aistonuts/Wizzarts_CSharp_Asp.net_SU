namespace MagicCardsmith.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static MagicCardsmith.Common.DataConstants;

    public class Art : BaseDeletableModel<string>
    {
        public Art()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(ArtTitleMaxLength)]
        [Comment("Art Title")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(ArtDescriptionMaxLength)]
        [Comment("Art Description")]
#nullable enable
        public string? Description { get; set; } = string.Empty;

        [Required]
        [Comment("Art URL")]
        public string RemoteImageUrl { get; set; } = string.Empty;

        [Comment("Art image extension")]
        public string Extension { get; set; } = string.Empty;

        [Comment("Is this art added by MagciCardsmith team.")]
        public int? ArtIstId { get; set; }

        public Artist Artist { get; set; }

        [Comment("Any cards using this art.")]
#nullable enable
        public int? CardId { get; set; }

        public Card Card { get; set; }

        [Comment("Is art added from an event")]
        public bool IsEventArt { get; set; }

        [Comment("Is art approved by admin")]
        public bool ApprovedByAdmin { get; set; }

        [Comment("User id")]
        public string? ApplicationUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
