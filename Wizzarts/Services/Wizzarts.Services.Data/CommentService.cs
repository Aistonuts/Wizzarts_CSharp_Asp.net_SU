using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Repositories;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels.PlayCard;

namespace Wizzarts.Services.Data
{

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<CommentCard> commentRepository;

        public CommentService(
            IDeletableEntityRepository<CommentCard> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task AttackCommentAsync(SingleCardViewModel input, string userId, string cardId, bool isAdmin)
        {
            var comment = new CommentCard
            {
                CardName = input.Name,
                CardFlavor = input.AbilitiesAndFlavor,
                Review = input.CommentReview,
                Suggestions = input.CommentSuggestions,
                CardId = cardId,
                PostedByUserId = userId,
            };
            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public Task DefenseReplyCommentAsync(SingleCardViewModel input, string userId, string cardId, int commentId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllAttackComments<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllAttackCommentsByAdmin<T>(string cardId)
        {
            var comment = this.commentRepository.AllAsNoTracking()
                .Where(x => x.CardId == cardId )
                .To<T>().ToList();

            return comment;
        }

        public IEnumerable<T> GetAllDefenseComments<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllDefenseCommentsByCardOwnerId<T>(string cardId, string userId)
        {
            throw new NotImplementedException();
        }

        public T GetAttackCommentById<T>(int id)
        {
            var comment = this.commentRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return comment;
        }
    }
}
