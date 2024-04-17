namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<Art> artRepository;
        private readonly IDeletableEntityRepository<Avatar> avatarRepository;

        public UserService(
            IDeletableEntityRepository<Art> artRepository,
            IDeletableEntityRepository<Avatar> avatarRepository)
        {
            this.artRepository = artRepository;
            this.avatarRepository = avatarRepository;

        }

        public IEnumerable<T> GetAllArtByUserId<T>(string id)
        {
            var art = this.artRepository.AllAsNoTracking()
               .Where(x => x.ApplicationUserId == id)
               .To<T>().ToList();

            return art;
        }

        public IEnumerable<T> GetAllAvatars<T>()
        {
            var avatars = this.avatarRepository.AllAsNoTracking()
                .To<T>().ToList();

            return avatars;
        }

        public int GetCountOfArt(string id)
        {
            var artCount = this.artRepository.AllAsNoTracking()
              .Where(x => x.ApplicationUserId == id)
              .Count();

            return artCount;
        }

        public Task<bool> HasArtByUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public bool HasEventParticipation(string userId)
        {
            throw new System.NotImplementedException();
        }

        public bool HasPublishedContent(string userId)
        {
            throw new System.NotImplementedException();
        }

        public bool IsArtist(string userId)
        {
            throw new System.NotImplementedException();
        }

        public bool IsContentCreator(string userId)
        {
            throw new System.NotImplementedException();
        }

        public bool IsStoreOwner(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
