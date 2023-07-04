// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using MagicCardsHub.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace MagicCardsHub.Areas.Identity.Pages.Account
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
    public class ClaimRole : PageModel
    {
        private readonly UserManager<ProjectCreator> _userManager;
        private readonly RoleManager<CreatorRole> _roleManager;

        public ClaimRole(
            UserManager<ProjectCreator> userManager,
            RoleManager<CreatorRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
           
        }

        public string ReturnUrl { get; set; }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
   
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(this.User.Id());
                  
           
                    await _userManager.RemoveFromRoleAsync(user, "User");
                    await _userManager.AddToRoleAsync(user, "Admin");

            }

            // If we got this far, something failed, redisplay form
            return Page();
        }   
      
    }
}
