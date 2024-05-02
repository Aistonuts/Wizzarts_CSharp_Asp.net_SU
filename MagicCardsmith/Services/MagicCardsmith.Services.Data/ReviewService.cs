using MagicCardsmith.Data.Models;
using System.Collections.Generic;

namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Expansion;

    public class ReviewService : IReviewService
    {
        private readonly IDeletableEntityRepository<CardReview> reviewRepository;

        public ReviewService(IDeletableEntityRepository<CardReview> reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task CreateAsync(SingleCardViewModel input, string userId, int id)
        {
            var review = new CardReview()
            {
                Title = input.Name,
                Description = input.AbilitiesAndFlavor,
                Review = input.Review,
                Suggestions = input.Suggestions,
                CardId = id,
                PostedByUserId = userId,
            };
            await this.reviewRepository.AddAsync(review);
            await this.reviewRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var art = this.reviewRepository.AllAsNoTracking()
              .Where(x => x.PostedByUserId == id)
              .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
              .To<T>().ToList();

            return art;
        }

        public IEnumerable<T> GetAllReviews<T>()
        {
            var reviews = this.reviewRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>().ToList();

            return reviews;
        }
    }
}