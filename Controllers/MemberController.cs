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
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {

        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(userManager, signInManager)
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
        public async Task<IActionResult> UserEdit(UserViewModel userViewModel, IFormFile userPicture)
        {
            ModelState.Remove("Password");
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));

            if (ModelState.IsValid)
            {
                AppUser user = CurrentUser;

                if (userPicture != null && userPicture.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/file", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
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

        public IActionResult CreatePasswordLoginWithFacebook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePasswordLoginWithFacebook(PasswordChangeViewModel passwordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                
                AppUser user = CurrentUser;

                IdentityResult result = _userManager.ChangePasswordAsync(user, passwordChangeViewModel.PasswordOld, passwordChangeViewModel.PasswordNew).Result;


                if (result.Succeeded)
                {
                    _userManager.UpdateSecurityStampAsync(user);
                    _signInManager.SignOutAsync();
                    _signInManager.PasswordSignInAsync(user, passwordChangeViewModel.PasswordNew, true, false);

                    ViewBag.success = "true";

                }
                else
                {
                    AddModelError(result);
                }

            }

            return View(passwordChangeViewModel);
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {

            if (ReturnUrl.Contains("ViolancePage"))
            {
                ViewBag.message = "Erişmeye çalıştığınız sayfa şiddet videoları içerdiğinden dolayı 15 yaşından büyük olmanız gerekmektedir.";
            }
            else if (ReturnUrl.Contains("AnkaraPage"))
            {
                ViewBag.message = "Bu sayfa sadece şehir alanı Ankara olan kullanıcılar için geçerlidir.";
            }
            else if (ReturnUrl.Contains("ExChange"))
            {
                ViewBag.message = "30 günlük ücretsiz deneme hakkınız sona ermiştir.";
            }
            else
            {
                ViewBag.message = "Bu sayfaya erişim izniniz yoktur. Erişim izni almak için site yöneticisiyle görüşünüz.";
            }




            return View();
        }

        [Authorize(Roles = "Editor,Admin")]
        public IActionResult Editor()
        {
            return View();
        }

        [Authorize(Roles = "Manager,Admin")]
        public IActionResult Manager()
        {
            return View();
        }

        [Authorize(Policy = "AnkaraPolicy")]
        //[Authorize(Policy = "AnkaraPolicy",Roles ="Editor")]
        public IActionResult AnkaraPage()
        {
            return View();
        }

        [Authorize(Policy = "ViolancePolicy")]
        public IActionResult ViolancePage()
        {
            return View();
        }

        public async Task<IActionResult> ExChangeRedirect()
        {
            bool result = User.HasClaim(x => x.Type == "ExpireDateExchange");

            if (!result)
            {
                Claim ExpireDateExchange = new Claim("ExpireDateExchange", DateTime.Now.AddDays(30).Date.ToShortDateString(), ClaimValueTypes.String, "Internal");

                await _userManager.AddClaimAsync(CurrentUser, ExpireDateExchange);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(CurrentUser, true);
            }


            return RedirectToAction("ExChange");
        }

        [Authorize(Policy = "ExchangePolicy")]
        public IActionResult ExChange()
        {
            return View();
        }
    }
}
