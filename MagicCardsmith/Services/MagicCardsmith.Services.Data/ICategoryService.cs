namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;

    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.SelectCategoriesViewModel;

    public interface ICategoryService
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
