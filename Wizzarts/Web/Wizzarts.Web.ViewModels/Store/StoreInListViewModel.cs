namespace Wizzarts.Web.ViewModels.Store
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class StoreInListViewModel : IndexAuthenticationViewModel, IMapFrom<Store>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public string StoreOwner { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Store, StoreInListViewModel>()
                .ForMember(x => x.StoreOwner, opt =>
                    opt.MapFrom(x =>
                        x.StoreOwner.UserName));
        }
    }
}
