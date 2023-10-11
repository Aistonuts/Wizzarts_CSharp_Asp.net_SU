namespace MagicCardsHub.Web.ViewModels.DigitalArt
{
    using System.Linq;

    using AutoMapper;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;

    public class SingleDigitalArtViewModel : IMapFrom<DigitalArt>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string DigitalArt { get; set; }

        public string ArtIstId { get; set; }

        public ApplicationUser Artist { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<DigitalArt, SingleDigitalArtViewModel>()
                .ForMember(x => x.DigitalArt, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/Images/digitalArt/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
