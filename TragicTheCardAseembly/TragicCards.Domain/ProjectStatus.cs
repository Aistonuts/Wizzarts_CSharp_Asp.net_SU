using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TragicCards.Domain
{
    public class ProjectStatus
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
    }
}
