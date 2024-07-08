namespace Wizzarts.Web.ViewModels.Article
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Home;

    public class ArticleListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<ArticleInListViewModel> Articles { get; set; }
    }
}
