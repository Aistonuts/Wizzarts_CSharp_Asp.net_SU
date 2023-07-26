namespace MagicCardsHub.Services.Data
{
    using System.Threading.Tasks;

    using MagicCardsHub.Web.ViewModels.Article;
    using MagicCardsHub.Web.ViewModels.GameProject;

    public interface IGameProjectService
    {
        Task CreateAsync(CreateGameProjectInputModel input, string userId, string imagePath);
    }
}
