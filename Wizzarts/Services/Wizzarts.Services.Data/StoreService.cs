namespace Wizzarts.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Store;

    public class StoreService : IStoreService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Store> storeRepository;

        public StoreService(IDeletableEntityRepository<Store> storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public async Task<string> ApproveStore(int id)
        {
            var store = await this.storeRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            store.ApprovedByAdmin = true;
            await this.storeRepository.SaveChangesAsync();
            return store.StoreOwnerId;
        }

        public async Task CreateAsync(CreateStoreViewModel input, string storeOwner, string imagePath)
        {
            var store = new Store
            {
                Name = input.StoreName,
                StoreOwnerId = storeOwner,
                Address = input.StoreAddress,
                City = input.StoreCity,
                Country = input.StoreCountry,
                PhoneNumber = input.StorePhoneNumber,
            };
            Directory.CreateDirectory($"{imagePath}/Stores/");
            var extension = Path.GetExtension(input.StoreImage.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/Stores/{store.Name.Replace(" ", "")}.{extension}";
            store.Image = $"/images/Stores/{store.Name.Replace(" ", "")}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.StoreImage.CopyToAsync(fileStream);
            await this.storeRepository.AddAsync(store);
            await this.storeRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var stores = await this.storeRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>().ToListAsync();

            return stores;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var store = await this.storeRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToListAsync();
            return store;
        }

        public async Task<IEnumerable<T>> GetAllApprovedStoresByUserId<T>(string id)
        {
            var stores = await this.storeRepository.AllAsNoTracking()
                 .OrderByDescending(x => x.Id)
                 .Where(x => x.ApprovedByAdmin == true && x.StoreOwnerId == id)
                 .To<T>().ToListAsync();

            return stores;
        }

        public async Task<IEnumerable<T>> GetAllStoresByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var store = await this.storeRepository.AllAsNoTracking()
           .Where(x => x.StoreOwnerId == id)
           .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
           .To<T>().ToListAsync();

            return store;
        }

        public async Task<T> GetById<T>(int id)
        {
            var store = await this.storeRepository.AllAsNoTracking()
                 .Where(x => x.Id == id)
                 .To<T>().FirstOrDefaultAsync<T>();

            return store;
        }

        public Task<int> GetCount()
        {
            return this.storeRepository.All().CountAsync();
        }
    }
}
