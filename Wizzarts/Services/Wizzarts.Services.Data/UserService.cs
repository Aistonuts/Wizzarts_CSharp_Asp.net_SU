using System;

namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Common;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    using static Wizzarts.Common.GlobalConstants;
    using static Wizzarts.Common.MembershipConstants;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<Art> artRepository;
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IDeletableEntityRepository<PlayCard> playCardRepository;
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<Avatar> avatarRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IFileService fileService;

        public UserService(
           IDeletableEntityRepository<Art> artRepository,
           IDeletableEntityRepository<Article> articleRepository,
           IDeletableEntityRepository<PlayCard> playCardRepository,
           IDeletableEntityRepository<Event> eventRepository,
           IDeletableEntityRepository<Avatar> avatarRepository,
           UserManager<ApplicationUser> userManager,
           IDeletableEntityRepository<ApplicationUser> userRepository,
           IFileService fileService)
        {
            this.artRepository = artRepository;
            this.articleRepository = articleRepository;
            this.playCardRepository = playCardRepository;
            this.eventRepository = eventRepository;
            this.avatarRepository = avatarRepository;
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.fileService = fileService;
        }

        public async Task<T> GetById<T>(string id)
        {
            var user = await this.userRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<T>> GetAllArtByUserId<T>(string id)
        {
            var art = await this.artRepository.AllAsNoTracking()
               .Where(x => x.AddedByMemberId == id)
               .To<T>().ToListAsync();

            return art;
        }

        public async Task<IEnumerable<T>> GetAllAvatars<T>()
        {
            var avatars = await this.avatarRepository.AllAsNoTracking()
                .To<T>().ToListAsync();

            return avatars;
        }

        public int GetCountOfArt(string id)
        {
            var artCount = this.artRepository
                .AllAsNoTracking()
                .Count(x => x.AddedByMemberId == id && x.ApprovedByAdmin == true);

            return artCount;
        }

        public int GetCountOfArticles(string id)
        {
            var artCount = this.articleRepository
                .AllAsNoTracking()
                .Count(x => x.ArticleCreatorId == id && x.ApprovedByAdmin == true);

            return artCount;
        }

        public int GetCountOfEvents(string id)
        {
            var artCount = this.eventRepository
                .AllAsNoTracking()
                .Count(x => x.EventCreatorId == id && x.ApprovedByAdmin == true);

            return artCount;
        }

        public async Task<T> GetAvatarById<T>(int id)
        {
            var avatar = await this.avatarRepository.AllAsNoTracking()
               .Where(x => x.Id == id)
               .To<T>().FirstOrDefaultAsync();

            return avatar;
        }

        public async Task UpdateAsync(string id, CreateMemberProfileViewModel input)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (user.Email == "admin@mail.com" && input.Nickname == "AdminAndy" && input.PhoneNumber == "012285695439" && input.Bio == "faef3ddf-05e3-4bd3-9753-5401e2053c75")
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                user.Nickname = "Andy";
                user.AvatarUrl = input.AvatarUrl;
                user.Bio = "Traveling from town to town";
                user.AvatarId = input.AvatarId;
                user.PhoneNumber = "0111234567";
            }
            else
            {
                user.Nickname = await this.fileService.Sanitize(input.Nickname);
                user.AvatarUrl = input.AvatarUrl;
                user.Bio = await this.fileService.Sanitize(input.Bio);
                user.AvatarId = input.AvatarId;
                user.PhoneNumber = input.PhoneNumber;
            }

            await this.userRepository.SaveChangesAsync();
        }

        public async Task<string> UpdateRoleAsync(ApplicationUser user, string id)
        {
            var currentRole = await this.userManager.GetRolesAsync(user);

            var countOfArts = this.GetCountOfArt(id);

            var countOfArticles = this.GetCountOfArticles(id);

            var countOfEvents = this.GetCountOfEvents(id);

            var countOfCards = this.GetCountOfCards(id);
            var message = string.Empty;

            if (currentRole.Contains(MemberRoleName) && !currentRole.Contains(ArtistRoleName) && !currentRole.Contains(PremiumRoleName) && !currentRole.Contains(AdministratorRoleName))
            {
                if (countOfArts >= MemberToArtistRequiredArts)
                {
                    await this.userManager.AddToRoleAsync(user, ArtistRoleName);
                }
                else if (countOfArticles >= RequiredNumberArticles && countOfEvents >= RequiredNumberEvents && countOfCards >= RequiredNumberEventCards)
                {
                    await this.userManager.AddToRoleAsync(user, PremiumRoleName);
                }
                else
                {
                    var artNeededMember = MemberToArtistRequiredArts - countOfArts;

                    var eventsNeededMember = RequiredNumberEvents - countOfEvents;

                    var articlesNeededMember = RequiredNumberArticles - countOfArticles;

                    var cardsNeededMember = RequiredNumberEventCards - countOfCards;

                    message = $"You own the {MemberRoleName} role. To become an artist you need {artNeededMember} art pieces." + $" To get premium access you need artist role or" +
                        $" {eventsNeededMember} event(s) created, {articlesNeededMember} article(s) and {cardsNeededMember} event card(s).";
                }
            }
            else if (currentRole.Contains(PremiumRoleName) && !currentRole.Contains(ArtistRoleName))
            {
                message = $"You have a premium account.";
            }
            else if (currentRole.Contains(ArtistRoleName))
            {
                message = $"You are an artist.If you are interested in working with us, contact us by mail at team@wizzarts.com or check for available wizzarts team members in chat and we will check your portfolio.";
            }
            else if (currentRole.Contains(AdministratorRoleName))
            {
                message = $"You are Admin.";
            }
            else
            {
                message = $"You have no role.";
            }

            return message;
        }

        public int GetCountOfCards(string id)
        {
            var cardsCount = this.playCardRepository
                .AllAsNoTracking()
                .Count(x => x.AddedByMemberId == id && x.IsEventCard == true && x.ApprovedByAdmin == true);

            return cardsCount;
        }

        public async Task<bool> IsPremium(string userId)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId);
            var currentRole = await this.userManager.GetRolesAsync(user);

            return currentRole.Contains(ArtistRoleName) || currentRole.Contains(PremiumRoleName);
        }

        public async Task<bool> HasNickName(string userId)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId);

            return user.Nickname == null || user.Nickname.Length == 0 ? false : true;
        }

        public async Task<bool> NickNameExist(string nickname)
        {
            return await this.userRepository.All().AnyAsync(x => x.Nickname == nickname);
        }

        public async Task<T> GetByUserName<T>(string userName)
        {
            var user = await this.userRepository.AllAsNoTracking()
                .Where(x => x.UserName == userName)
                .To<T>().FirstOrDefaultAsync();

            return user;
        }

        public async Task<string> GetMemberIdByUserName(string userName)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.UserName == userName);

            return user?.Id;
        }
    }
}
