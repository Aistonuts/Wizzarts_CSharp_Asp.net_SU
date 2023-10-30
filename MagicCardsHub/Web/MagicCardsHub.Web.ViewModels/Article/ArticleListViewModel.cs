namespace MagicCardsHub.Web.ViewModels.Article
{
    using System.Collections.Generic;

    public class ArticleListViewModel
    {
        public IEnumerable<ArticleInListViewModel> Articles { get; set; }
    }
}
