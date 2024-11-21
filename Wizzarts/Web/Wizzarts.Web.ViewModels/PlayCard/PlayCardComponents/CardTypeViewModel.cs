namespace Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class CardTypeViewModel : IMapFrom<PlayCardType>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
