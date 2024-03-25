namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;

    public interface IExpansionService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);
    }
}
