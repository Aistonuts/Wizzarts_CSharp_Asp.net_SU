namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWizzartsServices
    {
        Task<T> GetGameRules<T>(int id = 1);

        Task<IEnumerable<T>> GetAllGameRulesData<T>();

        Task<IEnumerable<T>> GetAllWizzartsTeamMembers<T>();

        string GetUserIdByArtistId(int artistId);
    }
}
