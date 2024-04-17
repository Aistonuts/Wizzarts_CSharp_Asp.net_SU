namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Stores;

    public interface IStoreService
    {
        IEnumerable<T> GetAll<T>();

        Task CreateAsync(CreateStoreInputModel input, string storeOwner, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        T GetById<T>(int id);

        int GetCount();
    }
}
