namespace MagicCardsmith.Web.ViewModels.Card
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.SelectCategoriesViewModel;
    using Microsoft.AspNetCore.Http;

    public class CreateCardInputModel : BaseCreateCardInputModel
    {
        public IFormFile Images { get; set; }

        public bool IsEventCard { get; set; }

        public int EventId { get; set; }

        public string EventMilestoneImage { get; set; }

        public string EventMilestoneTitle { get; set; }

        public string EventMilestoneDescription { get; set; }

        public string EventDescription { get; set; }

        public IEnumerable<ArtInListViewModel> ArtByUserId { get; set; } = new List<ArtInListViewModel>();

        public IEnumerable<BlackManaCostViewModel> BlackMana { get; set; } = new List<BlackManaCostViewModel>();

        public IEnumerable<BlueManCostViewModel> BlueMana { get; set; } = new List<BlueManCostViewModel>();

        public IEnumerable<GreenManaCostViewModel> GreenMana { get; set; } = new List<GreenManaCostViewModel>();

        public IEnumerable<RedManaCostViewModel> RedMana { get; set; } = new List<RedManaCostViewModel>();

        public IEnumerable<WhiteManaCostViewModel> WhiteMana { get; set; } = new List<WhiteManaCostViewModel>();

        public IEnumerable<ColorlessManaCostViewModel> ColorlessMana { get; set; } = new List<ColorlessManaCostViewModel>();

        public IEnumerable<CardTypeViewModel> SelectType { get; set; } = new List<CardTypeViewModel>();

        public IEnumerable<FrameColorViewModel> SelectFrameColor { get; set; } = new List<FrameColorViewModel>();

        public IEnumerable<ExpansionInListViewModel> SelectExpansion { get; set; } = new List<ExpansionInListViewModel>();
    }
}
