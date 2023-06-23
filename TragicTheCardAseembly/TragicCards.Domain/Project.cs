using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TragicCards.Domain
{
    public class Project
    {
        public Project()
        {
            this.Cards = new List<ProjectCard>();
            this.CardStats = new List<ProjectCardStat>();
            this.SeasonThemes = new List<ProjectSeasonTheme>();
        }   

        public ICollection<ProjectCard> Cards { get; set; }

        public ICollection<ProjectCardStat> CardStats { get; set; }

        public ICollection<ProjectSeasonTheme> SeasonThemes { get; set; }
    }
}
