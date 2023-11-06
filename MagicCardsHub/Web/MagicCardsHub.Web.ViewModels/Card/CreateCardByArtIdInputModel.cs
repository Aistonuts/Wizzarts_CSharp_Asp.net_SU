namespace MagicCardsHub.Web.ViewModels.Card
{
    using AutoMapper;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;

    public class CreateCardByArtIdInputModel : BaseCreateCardByArtIdInputModel , IMapFrom<Art>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public string AddedByArtistName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Art, CreateCardByArtIdInputModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.RemoteImageUrl != null ?
                        x.RemoteImageUrl :
                        "/Images/art/" + x.Id + "." + x.Extension))
                .ForMember(x => x.AddedByArtistName, opt =>
                    opt.MapFrom(x => x.Artist.UserName));
        }
    }
}
