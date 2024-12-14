namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public interface IUserService
    {
        Task<IEnumerable<T>> GetAllAvatars<T>();

        int GetCountOfArt(string id);

        int GetCountOfEvents(string id);

        int GetCountOfArticles(string id);

        int GetCountOfCards(string id);

        Task<IEnumerable<T>> GetAllArtByUserId<T>(string id);

        Task<T> GetAvatarById<T>(int id);

        Task<T> GetById<T>(string id);

        Task<T> GetByUserName<T>(string userName);

        Task UpdateAsync(string id, CreateMemberProfileViewModel input);

        Task<string> UpdateRoleAsync(ApplicationUser user, string id);

        Task<bool> IsPremium(string userId);

        Task<bool> HasNickName(string userId);

        Task<bool> NickNameExist(string nickname);
    }
}
