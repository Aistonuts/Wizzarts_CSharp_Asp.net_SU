namespace MagicCardsmith.Web.ViewModels.Expansion
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class ExpansionInListViewModel : IMapFrom<CardGameExpansion>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ExpansionSymbolUrl { get; set; }

        public string CardsCount { get; set; }

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
