using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Article;
using Wizzarts.Web.ViewModels.Event;
using Wizzarts.Web.ViewModels.Home;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.Store;

namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    public class MemberDataViewModel : IndexAuthenticationViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;
    }
}