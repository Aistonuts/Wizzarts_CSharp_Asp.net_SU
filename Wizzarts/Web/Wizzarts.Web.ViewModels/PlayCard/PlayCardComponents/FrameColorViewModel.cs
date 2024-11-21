namespace Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class FrameColorViewModel : IMapFrom<PlayCardFrameColor>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
