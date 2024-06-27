namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    public interface IPlayCardExpansionService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);
    }
}
