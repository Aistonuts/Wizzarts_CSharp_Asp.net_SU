using System.Collections.Generic;

namespace MagicCardsmith.Services.Data
{
    public interface IGameRulesService
    {
        T Get<T>(int id = 1);

        IEnumerable<T> GetAll<T>();
    }
}
