namespace Wizzarts.Web.ViewModels.PlayCard
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public class CreateCardViewModel : BaseCardViewModel
    {
        public IFormFile Images { get; set; }

        public int EventId { get; set; }

        public string EventMilestoneImage { get; set; } = string.Empty;

        public string EventMilestoneTitle { get; set; } = string.Empty;

        public string EventMilestoneDescription { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;

        public int EventCategoryId { get; set; }

        public IEnumerable<ArtInListViewModel> ArtByUserId { get; set; } = new List<ArtInListViewModel>();

        public IEnumerable<BlackManaCostViewModel> BlackMana { get; set; } = new List<BlackManaCostViewModel>();

        public IEnumerable<BlueManaCostViewModel> BlueMana { get; set; } = new List<BlueManaCostViewModel>();

        public IEnumerable<GreenManaCostViewModel> GreenMana { get; set; } = new List<GreenManaCostViewModel>();

        public IEnumerable<RedManaCostViewModel> RedMana { get; set; } = new List<RedManaCostViewModel>();

        public IEnumerable<WhiteManaCostViewModel> WhiteMana { get; set; } = new List<WhiteManaCostViewModel>();

        public IEnumerable<ColorlessManaCostViewModel> ColorlessMana { get; set; } = new List<ColorlessManaCostViewModel>();

        public IEnumerable<CardTypeViewModel> SelectType { get; set; } = new List<CardTypeViewModel>();

        public IEnumerable<FrameColorViewModel> SelectFrameColor { get; set; } = new List<FrameColorViewModel>();

        public IEnumerable<ExpansionInListViewModel> SelectExpansion { get; set; } = new List<ExpansionInListViewModel>();
    }
}
