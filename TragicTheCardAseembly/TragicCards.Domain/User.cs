using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TragicCards.Domain
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
