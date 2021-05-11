using ASP.NetCoreIdentity.Enums;
using ASP.NetCoreIdentity.Models;
using ASP.NetCoreIdentity.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {
        
        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager):base(userManager,signInManager)
        {
           
        } 
        public IActionResult Index()
        {
            AppUser user = CurrentUser;
            UserViewModel userViewModel = user.Adapt<UserViewModel>();


            return View(userViewModel);
        }

        public IActionResult UserEdit()
        {
            AppUser user = CurrentUser;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();

            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserViewModel userViewModel,IFormFile userPicture)
        {
            ModelState.Remove("Password");
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));

            if (ModelState.IsValid)
            {
                AppUser user = CurrentUser;

                if (userPicture!=null && userPicture.Length>0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/file", fileName);

                    using (var stream=new FileStream(path,FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);
                        user.Picture = "/file/" + fileName;
                    }
                }

                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.City = userViewModel.City;
                user.BirthDay = userViewModel.BirthDay;
                user.Gender = (int)userViewModel.Gender;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    ViewBag.success = "true";

                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);

                }
                else
                {
                    AddModelError(result);
                }
            }


            return View(userViewModel);
        }
        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = CurrentUser;


                bool existPassword = _userManager.CheckPasswordAsync(user, passwordChangeViewModel.PasswordOld).Result;

                if (existPassword)
                {
                    IdentityResult result = _userManager.ChangePasswordAsync(user, passwordChangeViewModel.PasswordOld, passwordChangeViewModel.PasswordNew).Result;

                    if (result.Succeeded)
                    {
                        _userManager.UpdateSecurityStampAsync(user);

                        //Identiy API 30 dk arayla client-side ile database arasında cookie değerinin aynı olup olmadığını kontrol eder. Parola değişimi olduğunda SecurityStamp değeri değişeceği için database kısmında cookie değeride değişecek. Identiy API cookie kontrolü yaptığında uyuşmazlık farkettiğinde kullanıcı Logout olur. Bunun yerine kullanıcı Logout olmadan backend kısmında SignOut/SignIn işlemi yapılır.

                        _signInManager.SignOutAsync();
                        _signInManager.PasswordSignInAsync(user, passwordChangeViewModel.PasswordNew, true, false);

                        ViewBag.success = "true";

                    }
                    else
                    {
                        AddModelError(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eski şifreniz yanlış!");
                }

            }




            return View(passwordChangeViewModel);
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles ="Editor,Admin")]
        public IActionResult Editor()
        {
            return View();
        }

        [Authorize(Roles = "Manager,Admin")]
        public IActionResult Manager()
        {
            return View();
        }
    }
}
