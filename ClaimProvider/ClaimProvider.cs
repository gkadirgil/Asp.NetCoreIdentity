using ASP.NetCoreIdentity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.ClaimProvider
{
    public class ClaimProvider : IClaimsTransformation
    {
        public UserManager<AppUser> _userManager;

        public ClaimProvider(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal != null && principal.Identity.IsAuthenticated)//kullanıcının üye olup olmadığı kontrolü yapılır
            {

                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;

                AppUser user = await _userManager.FindByNameAsync(identity.Name);

                if (user != null)
                {

                    if (user.BirthDay!=null)
                    {

                        var today = DateTime.Today;
                        var age = today.Year - user.BirthDay?.Year;

                        if (age>15)
                        {
                            Claim ViolanceClaim = new Claim("violance", true.ToString(), ClaimValueTypes.String, "Internal");

                            identity.AddClaim(ViolanceClaim);
                        }


                    }


                    if (user.City!=null)
                    {
                        if (!principal.HasClaim(c=>c.Type=="city"))
                        {
                            Claim CityClaim = new Claim("city", user.City, ClaimValueTypes.String, "Internal");
                            
                            identity.AddClaim(CityClaim);
                        }
                    }
                }
            }

            return principal;
        }
    }
}
