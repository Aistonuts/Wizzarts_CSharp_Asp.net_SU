using MagicCardsmith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Web.ViewModels.Card
{
    public abstract class BaseCreateCardInputModel
    {
        public string Name { get; set; }

        public int RedManaCost { get; set; }

        public int GreenManaCost { get; set; }

        public int BlueManaCost { get; set; }

        public int BlackManaCost { get; set; }

        public int WhiteManaCost { get; set; }

        public int ColorlessManaCost { get; set; }

        public string CardRemoteUrl { get; set; }

        public string FrameColor { get; set; }

        public string CardType { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string? Power { get; set; }

        public string? Toughness { get; set; }

        public string CardExpansion { get; set; }

        public string CardRarity { get; set; }

        public bool IsEventCard { get; set; } = true;

        public string ArtId { get; set; }

        public string CardSmithId { get; set; }

        public int GameExpansionId { get; set; }

    }
}
