namespace Wizzarts.Web.ViewModels.CardGameExpansion
{
    using System.Collections.Generic;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class SingleExpansionViewModel : IndexAuthenticationViewModel, IMapFrom<CardGameExpansion>, IHaveCustomMappings, ISingleExpansionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ExpansionSymbolUrl { get; set; } = string.Empty;

        public string CardsCount { get; set; } = string.Empty;

        public string ShippingLocation { get; set; } = string.Empty;

        public int StoreId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CardGameExpansion, SingleExpansionViewModel>()
                .ForMember(x => x.ExpansionSymbolUrl, opt =>
                    opt.MapFrom(x =>
                        x.ExpansionSymbolUrl != null ?
                        x.ExpansionSymbolUrl :
                        "/images/symbols/expansions/" + x.Title + "." + "png"));
        }
    }
}
