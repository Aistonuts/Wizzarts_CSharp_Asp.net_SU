namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    public interface IArtService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        IEnumerable<T> GetRandom<T>(int count);

        int GetCount();

        bool IsBase64String(string base64);
    }
}
