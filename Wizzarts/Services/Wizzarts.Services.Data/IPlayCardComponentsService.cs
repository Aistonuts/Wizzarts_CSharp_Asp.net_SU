using System.Threading.Tasks;

namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public interface IPlayCardComponentsService
    {
        Task<IEnumerable<BlackManaCostViewModel>> GetAllBlackMana();

        Task<IEnumerable<BlueManCostViewModel>> GetAllBlueMana();

        Task<IEnumerable<GreenManaCostViewModel>> GetAllGreenMana();

        Task<IEnumerable<RedManaCostViewModel>> GetAllRedMana();

        Task<IEnumerable<WhiteManaCostViewModel>> GetAllWhiteMana();

        Task<IEnumerable<ColorlessManaCostViewModel>> GetAllColorlessMana();

        Task<IEnumerable<CardTypeViewModel>> GetAllCardType();

        Task<IEnumerable<FrameColorViewModel>> GetAllCardFrames();

        Task<IEnumerable<ExpansionInListViewModel>> GetAllExpansionInListView();
    }
}
