namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.PlayCard;

    public interface ICommentService
    {
        Task CommentAsync(SingleCardViewModel input, string userId, string cardId, bool byAdmin);

        Task<IEnumerable<T>> GetAllCommentsByUser<T>(string userId);

        Task<IEnumerable<T>> GetCommentsByCardId<T>(string id);
    }
}
