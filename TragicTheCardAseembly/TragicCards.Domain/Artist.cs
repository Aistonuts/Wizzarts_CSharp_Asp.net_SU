using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TragicCards.Domain
{
    public class Artist
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public string webPage { get; set; }
        public string UserId { get; set; }

        public IEnumerable<Art> Art { get; init; } = new List<Art>();
    }
}
