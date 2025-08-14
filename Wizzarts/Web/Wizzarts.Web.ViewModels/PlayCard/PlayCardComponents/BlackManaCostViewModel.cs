using Microsoft.EntityFrameworkCore;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents
{
    public class BlackManaCostViewModel : IMapFrom<ManaCost>
    {
        public int Id { get; set; }

        [Comment("Mana color type.")]
        public string Color { get; set; } = string.Empty;

        [Comment("Mana remote image url.")]
        public string RemoteImageUrl { get; set; } = string.Empty;

        [Comment("Play Card Total Cost")]
        public int Cost { get; set; }
    }
}
