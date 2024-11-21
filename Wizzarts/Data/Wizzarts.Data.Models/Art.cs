namespace Wizzarts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

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
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(RemoteImageUrlMaxLength)]
        [Comment("Art url")]
        public string RemoteImageUrl { get; set; } = string.Empty;

        [Comment("Image extension")]
        public string Extension { get; set; } = string.Empty;

        [Comment("New art piece  has been approved or not")]
        public bool ApprovedByAdmin { get; set; }

        public string AddedByMemberId { get; set; }

        [Comment("Premium account only")]
        public bool ForMainPage { get; set; }

        [ForeignKey(nameof(AddedByMemberId))]
        public ApplicationUser AddedByMember { get; set; }
    }
}
