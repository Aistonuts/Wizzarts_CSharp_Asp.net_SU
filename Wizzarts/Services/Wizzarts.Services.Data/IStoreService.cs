namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    public interface IStoreService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();
    }
}
