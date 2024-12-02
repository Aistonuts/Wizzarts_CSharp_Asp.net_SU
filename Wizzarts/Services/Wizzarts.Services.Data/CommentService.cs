namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<CommentCard> commentRepository;

        public CommentService(
            IDeletableEntityRepository<CommentCard> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task CommentAsync(SingleCardViewModel input, string userId, string cardId, bool byAdmin)
        {
            var comment = new CommentCard
            {
                CardName = input.Name,
                CardFlavor = input.AbilitiesAndFlavor,
                Review = input.CommentReview,
                Suggestions = input.CommentSuggestions,
                CardId = cardId,
                PostedByUserId = userId,
                IsAdminComment = byAdmin,
            };
            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllCommentsByUser<T>(string userId)
        {
            var comment = await this.commentRepository.AllAsNoTracking()
                .Where(x => x.PostedByUserId == userId)
                .To<T>().ToListAsync();

            return comment;
        }

        public async Task<IEnumerable<T>> GetCommentsByCardId<T>(string id)
        {
            var comment = await this.commentRepository.AllAsNoTracking()
                .Where(x => x.CardId == id)
                .To<T>().ToListAsync();

            return comment;
        }
    }
}
