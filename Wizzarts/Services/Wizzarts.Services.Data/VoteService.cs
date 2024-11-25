using Microsoft.EntityFrameworkCore;

namespace Wizzarts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;

    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> votesRepository;

        public VoteService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVotes(string cardId)
        {
            return this.votesRepository.All()
               .Where(x => x.CardId == cardId)
               .Average(x => x.Value);
        }

        public async Task SetVoteAsync(string cardId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
               .FirstOrDefault(x => x.CardId == cardId && x.AddedByMemberId == userId);
            if (vote == null)
            {
                vote = new Vote
                {
                    CardId = cardId,
                    AddedByMemberId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
