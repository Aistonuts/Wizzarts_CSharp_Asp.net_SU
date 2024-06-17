namespace MagicCardsmith.Web.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateArticleInputModel : BaseCreateArticleInputModel
    {
        [Required(ErrorMessage = "Article image is required!")]
        public IFormFile ImageUrl { get; set; }
    }
}
