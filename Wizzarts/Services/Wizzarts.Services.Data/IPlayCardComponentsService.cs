namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public interface IPlayCardComponentsService
    {
        IEnumerable<BlackManaCostViewModel> GetAllBlackMana();

        IEnumerable<BlueManCostViewModel> GetAllBlueMana();

        IEnumerable<GreenManaCostViewModel> GetAllGreenMana();

        IEnumerable<RedManaCostViewModel> GetAllRedMana();

        IEnumerable<WhiteManaCostViewModel> GetAllWhiteMana();

        IEnumerable<ColorlessManaCostViewModel> GetAllColorlessMana();

        IEnumerable<CardTypeViewModel> GetAllCardType();

        IEnumerable<FrameColorViewModel> GetAllCardFrames();

        IEnumerable<ExpansionInListViewModel> GetAllExpansionInListView();
    }
}
