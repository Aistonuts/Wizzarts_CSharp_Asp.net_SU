namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public interface IPlayCardComponentsService
    {
        Task<IEnumerable<CardTypeViewModel>> GetAllCardType();

        Task<IEnumerable<FrameColorViewModel>> GetAllCardFrames();

        Task<IEnumerable<ExpansionInListViewModel>> GetAllExpansionInListView();

        Task<bool> ManaColorExistsAsync(int id);

        Task<bool> CardTypeExistsAsync(int id);

        Task<bool> CardFrameExistsAsync(int id);
    }
}
