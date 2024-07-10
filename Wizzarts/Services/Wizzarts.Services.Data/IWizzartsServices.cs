namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    public interface IWizzartsServices
    {
        T GetGameRules<T>(int id = 1);

        IEnumerable<T> GetAllGameRulesData<T>();

        IEnumerable<T> GetAllWizzartsTeamMembers<T>();
    }
}
