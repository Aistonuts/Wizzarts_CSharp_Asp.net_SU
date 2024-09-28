using AutoMapper;
using System.Collections.Generic;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.Event;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.Store;

namespace Wizzarts.Web.Areas.Administration.Models.User
{
    public class SingleUserViewModelAdminArea : PagingViewModel, IMapFrom<ApplicationUser>, IHaveCustomMappings
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


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, SingleUserViewModelAdminArea>()
            .ForMember(x => x.AvatarUrl, opt =>
               opt.MapFrom(x =>
                  x.AvatarUrl));
        }
    }
}
