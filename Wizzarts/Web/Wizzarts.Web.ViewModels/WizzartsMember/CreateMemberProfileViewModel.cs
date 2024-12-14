namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.DataConstants;

    public class CreateMemberProfileViewModel : IndexAuthenticationViewModel, IMapFrom<Avatar>, IHaveCustomMappings
    {
        public int AvatarId { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The field {0} must be between {2} and {1} characters long")]
        public string Nickname { get; set; } = null!;

        public string AvatarUrl { get; set; }

        [Comment("Information about the user")]
        public string Bio { get; set; }

        [Required]
        [StringLength(AgentPhoneMaxLength, MinimumLength = AgentPhoneMinLength, ErrorMessage = "The field {0} must be between {2} and {1} characters long")]
        public string PhoneNumber { get; set; } = null!;

        public IEnumerable<AvatarInListViewModel> Avatars { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Avatar, CreateMemberProfileViewModel>()
             .ForMember(x => x.AvatarUrl, opt =>
                opt.MapFrom(x =>
                   x.AvatarUrl));
        }
    }
}
