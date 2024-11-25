using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels;
using Wizzarts.Web.ViewModels.CardComments;
using Wizzarts.Web.ViewModels.PlayCard;
using Xunit;

namespace Wizzarts.Services.Data.Tests.CommentServiceTest
{
    public class CommentServiceTest : UnitTestBase
    {
        public CommentServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CommentAsyncShouldCreateACommentAndAddAttachItToCorrectCard()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            using var commentRepostiory = new EfDeletableEntityRepository<CommentCard>(data);
            var commentService = new CommentService(commentRepostiory);

            var comment = new SingleCardViewModel()
            {
                Name = "Ancestral Recall",
                AbilitiesAndFlavor = "Draw 3 cards or force opponent to draw 3 cards.",
                CommentReview = "Tested Great",
                CommentSuggestions = "Go Test",
            };
            var userId = "2738e787-5d57-4bc7-b0d2-287242f04695";
            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";

            await commentService.CommentAsync(comment, userId, cardId, true);

            var newComments = data.CardComments.FirstOrDefault(x => x.PostedByUserId == userId);

            Assert.Equal(newComments.CardId,cardId);
            Assert.Equal(1,data.CardComments.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllCommentsByUserShouldReturnTheRightCommentsAndCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            using var commentRepository = new EfDeletableEntityRepository<CommentCard>(data);
            var commentService = new CommentService(commentRepository);

            var comment = new SingleCardViewModel()
            {
                Name = "Ancestral Recall",
                AbilitiesAndFlavor = "Draw 3 cards or force opponent to draw 3 cards.",
                CommentReview = "Tested Great",
                CommentSuggestions = "Go Test",
            };
            var userId = "2738e787-5d57-4bc7-b0d2-287242f04695";
            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";

            await commentService.CommentAsync(comment, userId, cardId, true);

            var adminComments = await commentService.GetAllCommentsByUser<CardCommentInListViewModel>(userId);
            var adminComment = adminComments.FirstOrDefault( x => x.PostedByUserId == userId);
            Assert.Equal(1, adminComments.Count());
            Assert.Equal("Tested Great", adminComment.Review);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllCommentsCardIdShouldReturnTheRightCommentsAndCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            using var commentRepository = new EfDeletableEntityRepository<CommentCard>(data);
            var commentService = new CommentService(commentRepository);

            var comment = new SingleCardViewModel()
            {
                Name = "Ancestral Recall",
                AbilitiesAndFlavor = "Draw 3 cards or force opponent to draw 3 cards.",
                CommentReview = "Tested Great",
                CommentSuggestions = "Go Test",
            };
            var userId = "2738e787-5d57-4bc7-b0d2-287242f04695";
            var cardId = "c330fecf-61a9-4e03-8052-cd2b9583a251";

            await commentService.CommentAsync(comment, userId, cardId, true);

            var cardComments = await commentService.GetCommentsByCardId<CardCommentInListViewModel>(cardId);
            var cardComment = cardComments.FirstOrDefault(x => x.CardId == cardId);
            Assert.Equal(1, cardComments.Count());
            Assert.Equal("Tested Great", cardComment.Review);
            this.TearDownBase();
        }
    }
}
