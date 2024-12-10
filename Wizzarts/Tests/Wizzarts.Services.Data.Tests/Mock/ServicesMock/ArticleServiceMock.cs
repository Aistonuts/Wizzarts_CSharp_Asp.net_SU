namespace Wizzarts.Services.Data.Tests.Mock.ServicesMock
{
    using System.Collections.Generic;

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
