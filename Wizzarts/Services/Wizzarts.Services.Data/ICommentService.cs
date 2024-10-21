using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Web.ViewModels.PlayCard;

namespace Wizzarts.Services.Data
{
    public interface ICommentService
    {
        Task CommentAsync(SingleCardViewModel input, string userId, string cardId, bool isAdmin);

        IEnumerable<T> GetAllCommentsByUser<T>(string userId);

        IEnumerable<T> GetCommentsByCardId<T>(string id);
    }
}
