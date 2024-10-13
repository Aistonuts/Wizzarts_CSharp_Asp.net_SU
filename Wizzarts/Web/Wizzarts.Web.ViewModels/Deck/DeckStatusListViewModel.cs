namespace Wizzarts.Web.ViewModels.Deck
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class DeckStatusListViewModel : IMapFrom<DeckStatus>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
