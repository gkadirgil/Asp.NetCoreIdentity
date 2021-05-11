﻿using ASP.NetCoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.CustomTagHelpers
{
    [HtmlTargetElement("td",Attributes ="user-roles")]
    public class UserRolesName:TagHelper
    {
        public UserManager<AppUser> _userManager { get; set; }

        public UserRolesName(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HtmlAttributeName("user-roles")]
        public string UserId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);

            IList<string> roles=await _userManager.GetRolesAsync(user);

            string html = string.Empty;

            roles.ToList().ForEach(role =>
            {
                html += $"<span class='badge badge-info'>{role}</span>";
            });

            output.Content.SetHtmlContent(html);

            /* Custom oluşturulan TagHelper _ViewImport dosyasına eklenir.
            @addTagHelper ASP.NetCoreIdentity.CustomTagHelpers.*,ASP.NetCoreIdentity */
        }
    }
}
