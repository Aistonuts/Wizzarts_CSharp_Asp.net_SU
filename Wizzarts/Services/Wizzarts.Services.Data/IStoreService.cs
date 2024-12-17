namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Wizzarts.Web.ViewModels.Store;

    public interface IStoreService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task<IEnumerable<T>> GetAll<T>(int page, int itemsPerPage = 12);

        Task<int> GetCount();

        Task CreateAsync(CreateStoreViewModel input, string storeOwner, string imagePath);

        Task<IEnumerable<T>> GetAllStoresByUserId<T>(string id, int page, int itemsPerPage = 3);

        Task<IEnumerable<T>> GetAllStoresByUserIdPageless<T>(string id);

        Task<IEnumerable<T>> GetAllApprovedStoresByUserId<T>(string id);

        Task<string> ApproveStore(int id);

        Task<T> GetById<T>(int id);

        Task<bool> ExistsAsync(int id);
    }
}
