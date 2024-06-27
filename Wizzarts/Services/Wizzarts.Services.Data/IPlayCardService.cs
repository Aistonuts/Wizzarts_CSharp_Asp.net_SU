namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    public interface IPlayCardService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        IEnumerable<T> GetRandom<T>(int count);
    }
}
