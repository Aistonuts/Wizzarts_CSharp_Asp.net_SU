namespace Wizzarts.Web.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateArticleViewModel : BaseArticleViewModel
    {
        [Required(ErrorMessage = "Article image is required!")]
        public IFormFile ImageUrl { get; set; }
    }
}
