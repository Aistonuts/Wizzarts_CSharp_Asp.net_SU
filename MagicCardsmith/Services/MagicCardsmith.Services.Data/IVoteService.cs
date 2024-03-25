namespace MagicCardsmith.Services.Data
{
    using System.Threading.Tasks;

    public interface IVoteService
    {
        Task SetVoteAsync(int reviewId, string userId, byte value);

        double GetAverageVotes(int reviewId);
    }
}
