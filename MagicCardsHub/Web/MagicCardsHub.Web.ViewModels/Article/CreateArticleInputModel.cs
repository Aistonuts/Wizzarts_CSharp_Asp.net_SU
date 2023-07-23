namespace MagicCardsHub.Web.ViewModels.Article
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateArticleInputModel
    {
        [MinLength(3)]
        public string Title { get; set; }

        [MinLength(10)]
        public string Description { get; set; }

        public IEnumerable<IFormFile> Image { get; set; }
    }
}
