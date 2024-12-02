namespace Wizzarts.Services.Data.Tests.Mock.ServicesMock
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using Wizzarts.Web.ViewModels.Article;

    public class ArticleServiceMock
    {
        public static IArticleService MockArticleService
        {
            get
            {
                var moq = new Mock<IArticleService>();

                moq.Setup(a => a.GetRandom<ArticleInListViewModel>(It.IsAny<int>()))
                    .Returns(new List<ArticleInListViewModel>());

                return moq.Object;
            }
        }
    }
}