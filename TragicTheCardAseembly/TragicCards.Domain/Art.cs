using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TragicCards.Domain
{
    public class Art
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Description { get; set; }

        public string ArtIstId { get; set; }

        public Artist Artist{ get; set; }

        public string ProjectCardId { get; set; }

        public ProjectCard ProjectCard { get; set; }
    }
}
