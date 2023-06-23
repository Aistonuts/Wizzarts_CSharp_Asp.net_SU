using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAssembler.Domain
{
    public class CardStats
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public string Description { get; set; }           
        public int Damage { get; set; }
        public int Defence { get;set; }

    }
}
