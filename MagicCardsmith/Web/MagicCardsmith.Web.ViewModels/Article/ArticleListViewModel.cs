namespace MagicCardsmith.Web.ViewModels.Article
{
    using System.Collections.Generic;

    public class ArticleListViewModel : PagingViewModel
    {
        public IEnumerable<ArticleInListViewModel> Art { get; set; }
    }
}
