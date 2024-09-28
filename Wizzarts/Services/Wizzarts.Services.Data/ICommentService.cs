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
        Task AttackCommentAsync(SingleCardViewModel input, string userId, string cardId, bool isAdmin);

        Task DefenseReplyCommentAsync(SingleCardViewModel input, string userId, string cardId, int commentId, bool isAdmin);

        IEnumerable<T> GetAllAttackComments<T>();

        IEnumerable<T> GetAllDefenseComments<T>();

        IEnumerable<T> GetAllDefenseCommentsByCardOwnerId<T>(string cardId, string userId);

        IEnumerable<T> GetAllAttackCommentsByAdmin<T>(string cardId);

        T GetAttackCommentById<T>(int id);
    }
}
