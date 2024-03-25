using System.Collections.Generic;

namespace MagicCardsmith.Services.Data
{
    using System.Threading.Tasks;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Expansion;

    public interface IReviewService
    {
        Task CreateAsync(SingleCardViewModel input, string userId, int id);

        IEnumerable<T> GetAllReviews<T>();
    }
}