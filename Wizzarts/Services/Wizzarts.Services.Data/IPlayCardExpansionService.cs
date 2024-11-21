namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlayCardExpansionService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);
    }
}
