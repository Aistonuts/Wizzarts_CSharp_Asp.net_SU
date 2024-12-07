namespace Wizzarts.Web.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;

    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.DataConstants;

    public class BaseArticleViewModel : IndexAuthenticationViewModel, ISingleArticleViewModel
    {
        [Required(ErrorMessage = "Article title is required!")]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength, ErrorMessage = "Article title should be between 5 and 50 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Article short description is required!")]
        [StringLength(ArticleShortDescriptionMaxLength, MinimumLength = ArticleShortDescriptionMinLength, ErrorMessage = "Article short description should be between 10 and 500 characters long")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Article description is required!")]
        [StringLength(ArtDescriptionMaxLength, MinimumLength = ArticleDescriptionMinLength, ErrorMessage = "Article description should be between 10 and 1000 characters long")]
        public string Description { get; set; }
    }
}
