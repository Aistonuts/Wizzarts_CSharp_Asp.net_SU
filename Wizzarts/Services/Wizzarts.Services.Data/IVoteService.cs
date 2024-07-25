namespace Wizzarts.Services.Data
{
    using System.Threading.Tasks;

    public interface IVoteService
    {
        Task SetVoteAsync(string cardId, string userId, byte value);

        double GetAverageVotes(string cardId);
    }
}
