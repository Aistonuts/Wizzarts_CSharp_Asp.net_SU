using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TragicCards.Domain
{
    public class ProjectCard
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();        
    
        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string StatusId { get; set; }

        public ProjectStatus Status { get; set; }

        public string ArtId { get; set; }

        public Art Art { get; set; }

        public string CardStatsId { get; set; }

        public ProjectCardStat CardStats { get; set; }
    }
}
