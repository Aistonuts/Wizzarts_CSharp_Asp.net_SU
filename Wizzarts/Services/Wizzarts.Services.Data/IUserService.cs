namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wizzarts.Data.Models;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public interface IUserService
    {
        IEnumerable<T> GetAllAvatars<T>();

        int GetCountOfArt(string id);

        int GetCountOfEvents(string id);

        int GetCountOfArticles(string id);

        int GetCountOfCards(string id);

        IEnumerable<T> GetAllArtByUserId<T>(string id);

        T GetAvatarById<T>(int id);

        T GetById<T>(string id);

        Task UpdateAsync(string id, CreateMemberProfileViewModel input);

        Task<string> UpdateRoleAsync(ApplicationUser user, string id);

    }
}
