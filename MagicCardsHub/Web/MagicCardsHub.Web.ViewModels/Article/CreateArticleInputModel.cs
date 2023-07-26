namespace MagicCardsHub.Web.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;

    using MagicCardsHub.Web.ViewModels.BaseCreateModel;

    public class CreateArticleInputModel : BaseCreateImageInputModel
    {
        [MinLength(3)]
        public string Title { get; set; }

        [MinLength(10)]
        public string Description { get; set; }
    }
}
