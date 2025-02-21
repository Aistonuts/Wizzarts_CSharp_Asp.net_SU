namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public interface IPlayCardComponentsService
    {
        Task<IEnumerable<BlackManaCostViewModel>> GetAllBlackMana();

        Task<IEnumerable<BlueManaCostViewModel>> GetAllBlueMana();

        Task<IEnumerable<GreenManaCostViewModel>> GetAllGreenMana();

        Task<IEnumerable<RedManaCostViewModel>> GetAllRedMana();

        Task<IEnumerable<WhiteManaCostViewModel>> GetAllWhiteMana();

        Task<IEnumerable<ColorlessManaCostViewModel>> GetAllColorlessMana();

        Task<IEnumerable<CardTypeViewModel>> GetAllCardType();

        Task<IEnumerable<FrameColorViewModel>> GetAllCardFrames();

        Task<IEnumerable<ExpansionInListViewModel>> GetAllExpansionInListView();

        Task<bool> BlackManaExistsAsync(int id);

        Task<bool> BlueManaExistsAsync(int id);

        Task<bool> GreenManaExistsAsync(int id);

        Task<bool> RedManaExistsAsync(int id);

        Task<bool> WhiteManaExistsAsync(int id);

        Task<bool> ColorlessManaExistsAsync(int id);

        Task<bool> CardTypeExistsAsync(int id);

        Task<bool> CardFrameExistsAsync(int id);
    }
}
