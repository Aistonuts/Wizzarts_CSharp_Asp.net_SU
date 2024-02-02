namespace MagicCardsmith.Web.ViewModels.Article
{
    using Microsoft.AspNetCore.Http;

    public class CreateArticleInputModel : BaseCreateArticleInputModel
    {
        public IFormFile ImageUrl { get; set; }
    }
}
