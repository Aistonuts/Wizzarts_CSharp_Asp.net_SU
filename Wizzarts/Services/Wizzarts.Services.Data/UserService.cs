namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Wizzarts.Data;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    using static Wizzarts.Common.MembershipConstants;
    using static Wizzarts.Common.GlobalConstants;
    using Wizzarts.Data.Seeding;
    using Wizzarts.Web.ViewModels.Store;
    using Wizzarts.Common;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<Art> artRepository;
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IDeletableEntityRepository<PlayCard> playCardRepository;
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<Avatar> avatarRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IStoreService storeService;
        public UserService(
           IDeletableEntityRepository<Art> artRepository,
           IDeletableEntityRepository<Article> articleRepository,
           IDeletableEntityRepository<PlayCard> playCardRepository,
           IDeletableEntityRepository<Event> eventRepository,
           IDeletableEntityRepository<Avatar> avatarRepository,
           UserManager<ApplicationUser> userManager,
           IDeletableEntityRepository<ApplicationUser> userRepository,
           IStoreService storeService)
        {
            this.artRepository = artRepository;
            this.articleRepository = articleRepository;
            this.playCardRepository = playCardRepository;
            this.eventRepository = eventRepository;
            this.avatarRepository = avatarRepository;
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.storeService = storeService;
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
            user.Nickname = input.Nickname;
            user.AvatarUrl = input.AvatarUrl;
            user.Bio = input.Bio;
            user.AvatarId = input.AvatarId;

            await this.userRepository.SaveChangesAsync();
        }

        public async Task<string> UpdateRoleAsync(ApplicationUser user, string id)
        {
            var currentRole = await this.userManager.GetRolesAsync(user);

            var countOfArts = this.GetCountOfArt(id);

            var countOfArticles = this.GetCountOfArticles(id);

            var countOfEvents = this.GetCountOfEvents(id);

            var countOfCards = this.GetCountOfCards(id);

            string message = " ";

            if (currentRole.Contains(StoreOwnerRoleName) && !currentRole.Contains(ContentCreatorRoleName))
            {
                if(currentRole.Contains(ArtistRoleName))
                {
                    if (!currentRole.Contains(ContentCreatorRoleName))
                    {
                        await this.userManager.AddToRoleAsync(user, ContentCreatorRoleName);
                        message = "Congratulations you have acquired content creator role . Check your benefits in the membership section";
                    }
                }
            }
           else if (currentRole.Contains(ArtistRoleName) && !currentRole.Contains(ContentCreatorRoleName))
            {
                if (countOfArts >= ArtistToContentCreatorRequiredArts && !currentRole.Contains(ContentCreatorRoleName))
                {
                    await this.userManager.AddToRoleAsync(user, ContentCreatorRoleName);
                    await this.userManager.RemoveFromRoleAsync(user, ArtistRoleName);
                    message = "Congratulations you have acquired content creator role . Check your benefits in the membership section";
                }
               else if (countOfArticles >= RequiredNumberArticles && countOfEvents >= RequiredNumberEvents && countOfCards >= RequiredNumberEventCards && !currentRole.Contains(ContentCreatorRoleName))
                {
                    await this.userManager.AddToRoleAsync(user, ContentCreatorRoleName);
                    await this.userManager.RemoveFromRoleAsync(user, ArtistRoleName);
                    message = "Congratulations you have acquired content creator role. Check your benefits in the membership section";
                }
                else
                {
                    var artNeededArtist = ArtistToContentCreatorRequiredArts - countOfArts;

                    var eventsNeededArtist = RequiredNumberEvents - countOfEvents;

                    var articlesNeededArtist = RequiredNumberArticles - countOfArticles;

                    var cardsNeededArtist = RequiredNumberEventCards - countOfCards;

                    message = $"You own the {ArtistRoleName} role. Keep up the good work. To become a content creator you need {artNeededArtist} art pieces." + $" To become a content creator you will need 13 art pieces in total or" +
                       $" {eventsNeededArtist} event(s) created, {articlesNeededArtist} article(s) and {cardsNeededArtist} event card(s).";
                }
            }
           else if (currentRole.Contains(MemberRoleName) && !currentRole.Contains(ContentCreatorRoleName) && !currentRole.Contains(ArtistRoleName))
            {
                if (countOfArts >= MemberToArtistRequiredArts && !currentRole.Contains(ArtistRoleName))
                {
                    await this.userManager.AddToRoleAsync(user, ArtistRoleName);
                    await this.userManager.RemoveFromRoleAsync(user, MemberRoleName);
                    message = "Congratulations you have acquired artist role. Check your benefits in the membership section";
                }
                else if (countOfArticles >= RequiredNumberArticles && countOfEvents >= RequiredNumberEvents && countOfCards >= RequiredNumberEventCards )
                {
                    await this.userManager.AddToRoleAsync(user, ArtistRoleName);
                    await this.userManager.RemoveFromRoleAsync(user, MemberRoleName);
                    message = "Congratulations you have acquired artist role. Check your benefits in the membership section";
                }
                else
                {
                    var artNeededMember = MemberToArtistRequiredArts - countOfArts;

                    var eventsNeededMember = RequiredNumberEvents - countOfEvents;

                    var articlesNeededMember = RequiredNumberArticles - countOfArticles;

                    var cardsNeededMember = RequiredNumberEventCards - countOfCards;

                    message = $"You own the {MemberRoleName}. To become an artist you need {artNeededMember} art pieces." + $" To become a content creator you will need 13 total of art pieces or" +
                        $" {eventsNeededMember} event(s) created, {articlesNeededMember} articles and {cardsNeededMember} event cards.";
                }
            }

            return message.ToString();
        }

        public int GetCountOfCards(string id)
        {
            var cardsCount = this.playCardRepository.AllAsNoTracking()
             .Where(x => x.AddedByMemberId == id && x.ApprovedByAdmin == true && x.IsEventCard == true)
             .Count();

            return cardsCount;
        }
    }
}
