namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<Art> artRepository;
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<Avatar> avatarRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UserService(
           IDeletableEntityRepository<Art> artRepository,
           IDeletableEntityRepository<Article> articleRepository,
           IDeletableEntityRepository<Event> eventRepository,
           IDeletableEntityRepository<Avatar> avatarRepository,
           UserManager<ApplicationUser> userManager,
           IDeletableEntityRepository<ApplicationUser> userRepositor)
        {
            this.artRepository = artRepository;
            this.articleRepository = articleRepository;
            this.eventRepository = eventRepository;
            this.avatarRepository = avatarRepository;
            this.userManager = userManager;
            this.userRepository = userRepositor;
        }

        public T GetById<T>(string id)
        {
            var user = this.userRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return user;
        }

        public IEnumerable<T> GetAllArtByUserId<T>(string id)
        {
            var art = this.artRepository.AllAsNoTracking()
               .Where(x => x.AddedByMemberId == id)
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
              .Where(x => x.AddedByMemberId == id && x.ApprovedByAdmin == true)
              .Count();

            return artCount;
        }

        public int GetCountOfArticles(string id)
        {
            var artCount = this.articleRepository.AllAsNoTracking()
              .Where(x => x.ArticleCreatorId == id && x.ApprovedByAdmin == true)
              .Count();

            return artCount;
        }

        public int GetCountOfEvents(string id)
        {
            var artCount = this.eventRepository.AllAsNoTracking()
              .Where(x => x.EventCreatorId == id && x.ApprovedByAdmin == true)
              .Count();

            return artCount;
        }

        public T GetAvatarById<T>(int id)
        {
            var avatar = this.avatarRepository.AllAsNoTracking()
               .Where(x => x.Id == id)
               .To<T>().FirstOrDefault();

            return avatar;
        }

        public async Task UpdateAsync(string id, CreateMemberProfileViewModel input)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == id);
            //user.Nickname = input.Nickname;
            //user.AvatarUrl = input.AvatarUrl;

            await this.userRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllAsync<T>(int page, int itemsPerPage = 12)
        {
            throw new System.NotImplementedException();
        }
    }
}
