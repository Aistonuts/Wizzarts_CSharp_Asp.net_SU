using System.Threading.Tasks;

namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Store;

    public class StoreService : IStoreService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Store> storeRepository;
        private readonly IFileService fileService;

        public StoreService(
            IDeletableEntityRepository<Store> storeRepository,
            IFileService fileService)
        {
            this.storeRepository = storeRepository;
            this.fileService = fileService;
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
                Name = await this.fileService.Sanitize(input.StoreName),
                StoreOwnerId = storeOwner,
                Address = await this.fileService.Sanitize(input.StoreAddress),
                City = await this.fileService.Sanitize(input.StoreCity),
                Country = await this.fileService.Sanitize(input.StoreCountry),
                PhoneNumber = input.StorePhoneNumber,
            };

            if (await this.fileService.IsValidImage(input.StoreImage) == false)
            {
                throw new Exception($"Invalid image");
            }

            Directory.CreateDirectory($"{imagePath}/Stores/");
            var extension = Path.GetExtension(input.StoreImage.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/Stores/{store.Name.Replace(" ", string.Empty)}.{extension}";
            store.Image = $"/images/Stores/{store.Name.Replace(" ", string.Empty)}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

            await input.StoreImage.CopyToAsync(fileStream);

            await this.storeRepository.AddAsync(store);
            await this.storeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var store = await this.storeRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (store != null)
            {
                this.storeRepository.Delete(store);
                await this.storeRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await this.storeRepository.AllAsNoTracking()
                .AnyAsync(x => x.Id == id);
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
                .Where(x => x.ApprovedByAdmin == true)
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

        public async Task<IEnumerable<T>> GetAllStoresByUserIdPageless<T>(string id)
        {
            var store = await this.storeRepository.AllAsNoTracking()
           .Where(x => x.StoreOwnerId == id)
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

        public async Task<int> GetCount()
        {
            return await this.storeRepository.All().CountAsync(x => x.ApprovedByAdmin == true);
        }

        public async Task<bool> HasUserWithIdAsync(int id, string userId)
        {
            return await this.storeRepository.AllAsNoTracking()
                  .AnyAsync(a => a.Id == id && a.StoreOwnerId == userId);
        }

        public async Task<bool> StoreNameExist(string name)
        {
            return await this.storeRepository
            .AllAsNoTracking().AnyAsync(a => a.Name == name);
        }

        public async Task UpdateAsync(int id, EditStoreViewModel input)
        {
            var store = await this.storeRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (store != null)
            {
                store.Name = input.StoreName;
                store.Country = input.StoreCountry;
                store.City = input.StoreCity;
                store.PhoneNumber = input.StorePhoneNumber;
                store.Address = input.StoreAddress;

                await this.storeRepository.SaveChangesAsync();
            }
        }
    }
}