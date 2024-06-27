namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    public interface IArticleService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3);

        IEnumerable<T> GetRandom<T>(int count);

        int GetCount();
    }
}
