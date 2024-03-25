namespace MagicCardsmith.Services.Data
{
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> votesRepository;

        public VoteService(IRepository<Vote> votesRepositor)
        {
            this.votesRepository = votesRepositor;
        }

        public double GetAverageVotes(int cardId)
        {
            return this.votesRepository.All()
                .Where(x => x.CardId == cardId)
                .Average(x => x.Value);
        }

        public async Task SetVoteAsync(int cardId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.CardId == cardId && x.UserId == userId);
            if (vote == null)
            {
                vote = new Vote
                {
                    CardId = cardId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
