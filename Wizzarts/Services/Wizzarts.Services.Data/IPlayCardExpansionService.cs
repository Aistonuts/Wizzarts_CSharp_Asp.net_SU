namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlayCardExpansionService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task<T> GetById<T>(int id);
    }
}
