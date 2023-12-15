namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Artist;
    using Microsoft.EntityFrameworkCore;

    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDeletableEntityRepository<Artist> artistRepository;

        public ArtistService(
            ApplicationDbContext dbContext,
            IDeletableEntityRepository<Artist> artistRepository)
        {
            this.dbContext = dbContext;
            this.artistRepository = artistRepository;
        }

        public async Task<bool> ArtistExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Artists
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }

        public async Task Create(string userId, BecomeArtistViewModel model)
        {
                Artist newAgent = new Artist()
                {
                    Nickname = model.Nickname,
                    Email = model.Email,
                    AvatarUrl = model.AvatarUrl,
                    UserId = userId,
                };

                await this.artistRepository.AddAsync(newAgent);
                await this.artistRepository.SaveChangesAsync();

        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var artists = this.artistRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();

            return artists;
        }

        public async Task<string> GetArtistIdByUserIdAsync(string userId)
        {
            Artist? agent = await this.dbContext
                .Artists
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (agent == null)
            {
                return null;
            }

            return agent.Id.ToString();
        }

        public T GetById<T>(int id)
        {
            var artist = this.artistRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return artist;
        }

        public int GetCount()
        {
            return this.artistRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.artistRepository.All()
              .OrderBy(x => Guid.NewGuid())
              .Take(count)
              .To<T>().ToList();
        }

        public async Task<bool> HasArtByUserIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return false;
            }

            return user.Art.Any();
        }
    }
}