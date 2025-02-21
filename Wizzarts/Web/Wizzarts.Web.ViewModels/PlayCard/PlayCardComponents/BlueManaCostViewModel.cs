using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents
{
    public class BlueManaCostViewModel : IMapFrom<ManaCost>
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
