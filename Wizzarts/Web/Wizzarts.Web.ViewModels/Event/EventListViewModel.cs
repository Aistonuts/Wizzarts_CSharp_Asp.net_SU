using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Web.ViewModels.Home;

namespace Wizzarts.Web.ViewModels.Event
{
    public class EventListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<EventInListViewModel> Events { get; set; }
    }
}
