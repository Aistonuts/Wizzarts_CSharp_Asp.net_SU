namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Store;

    public interface IStoreService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        Task CreateAsync(CreateStoreViewModel input, string storeOwner, string imagePath);

        IEnumerable<T> GetAllStoresByUserId<T>(string id, int page, int itemsPerPage = 3);

        IEnumerable<T> GetAllApprovedStoresByUserId<T>(string id);

        Task<string> ApproveStore(int id);
    }
}
