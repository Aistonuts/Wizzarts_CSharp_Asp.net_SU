using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents
{
    public class CardTypeViewModel : IMapFrom<PlayCardType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
