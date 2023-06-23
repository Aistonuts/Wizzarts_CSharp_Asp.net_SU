using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TragicCards.Domain
{
    public class ProjectCardStat
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public string Description { get; set; }
            
        public int Damage { get; set; }
        public int Hp { get;set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
