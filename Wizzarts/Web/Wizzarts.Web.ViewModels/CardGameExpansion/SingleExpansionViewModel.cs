namespace Wizzarts.Web.ViewModels.CardGameExpansion
{
    using AutoMapper;
    using System.Collections.Generic;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class SingleExpansionViewModel : IndexAuthenticationViewModel, IMapFrom<CardGameExpansion>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ExpansionSymbolUrl { get; set; }

        public string CardsCount { get; set; }

        public IEnumerable<CardInListViewModel> PlayCards { get; set; }

        public IEnumerable<ExpansionInListViewModel> Expansions { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CardGameExpansion, ExpansionInListViewModel>()
                .ForMember(x => x.ExpansionSymbolUrl, opt =>
                    opt.MapFrom(x =>
                        x.ExpansionSymbolUrl != null ?
                        x.ExpansionSymbolUrl :
                        "/images/symbols/expansions/" + x.Title + "." + "png"));
        }
    }
}
