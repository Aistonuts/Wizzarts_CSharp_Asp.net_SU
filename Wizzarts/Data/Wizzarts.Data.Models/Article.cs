namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class Article : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        [Comment("Article title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ArticleShortDescriptionMaxLength)]
        [Comment("Article short description")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        [MaxLength(ArticleDescriptionMaxLength)]
        [Comment("Article description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(RemoteImageUrlMaxLength)]
        [Comment("Article image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Comment("Is Article approved by Admin.")]
        public bool ApprovedByAdmin { get; set; }

        public bool ForMainPage { get; set; }

        [Required]
        [Comment("Article creator identifier")]
        public string ArticleCreatorId { get; set; } = string.Empty;

        [ForeignKey(nameof(ArticleCreatorId))]
        public ApplicationUser ArticleCreator { get; set; }
    }
}
