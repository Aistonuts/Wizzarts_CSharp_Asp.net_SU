using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.Card;
using Wizzarts.Web.ViewModels.Event;
using Wizzarts.Web.ViewModels.Store;

namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    public class MemberDataViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public IEnumerable<ArtInListViewModel> Arts { get; set; }

        public IEnumerable<ArticleInListViewModel> Articles { get; set; }

        public IEnumerable<EventInListViewModel> Events { get; set; }

        public IEnumerable<CardInListViewModel> Cards { get; set; }

        public IEnumerable<StoreInListViewModel> Stores { get; set; }
    }
}