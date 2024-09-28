using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Event;
using Wizzarts.Web.ViewModels.Home;
using Wizzarts.Web.ViewModels.Store;
using Wizzarts.Web.ViewModels.WizzartsMember;

namespace Wizzarts.Web.ViewModels.Deck
{
    public class BaseDeckViewModel : IndexAuthenticationViewModel, IMapFrom<CardDeck>/*, IHaveCustomMappings*/
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShippingAddress { get; set; }

        public int StoreId { get; set; }

        public int EventId { get; set; }

        public IEnumerable<ArtInListViewModel> DeckLogos { get; set; }

        public IEnumerable<DeckInListViewModel> Decks { get; set; }

        public IEnumerable<EventComponentsInListViewModel> EventComponents { get; set; }
        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
