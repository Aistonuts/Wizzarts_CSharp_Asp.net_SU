using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Web.ViewModels.Deck
{
    public class DeckInListViewModel : IMapFrom<CardDeck>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShippingAddress { get; set; }

        public int StoreId { get; set; }

        public string ImageUrl { get; set; }
    }
}
