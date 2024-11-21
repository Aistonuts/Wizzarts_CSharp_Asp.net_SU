namespace Wizzarts.Web.ViewModels.Expansion
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class ExpansionInListViewModel : IMapFrom<CardGameExpansion>, IHaveCustomMappings, ISingleExpansionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ExpansionSymbolUrl { get; set; } = string.Empty;

        public string CardsCount { get; set; } = string.Empty;

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
