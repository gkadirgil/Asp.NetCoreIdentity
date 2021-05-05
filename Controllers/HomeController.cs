using ASP.NetCoreIdentity.Models;
using ASP.NetCoreIdentity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;

                IdentityResult result = await _userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");

                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }


            return View(userViewModel);
        }

        public IActionResult Login(string RerturnUrl)
        {
            TempData["Returnurl"] = RerturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(userLogin.Email);

                if (user != null)
                {
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınız bir süreliğine kilitlenmiştir. Liütfen daha sonra tekrar deneyiniz.");

                        return View(userLogin);
                    }
                    await _signInManager.SignOutAsync();
                    //daha önceden var olan cookie siler

                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, false);

                    //3.değer(isPersistant): true olursa cookie expiration süresi başlar. beni hatırla kısmı işaretlendiğinde true değerine çekilir.
                    //4.değer(lockoutOnFailure): kullanıcı login işleminde parola yanlış girdiğinde hesabı kitleme fonksiyonudur.

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);

                        if (TempData["Returnurl"] != null)
                        {
                            return Redirect(TempData["Returnurl"].ToString());
                        }

                        return RedirectToAction("Index", "Member");
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);

                        int failCount = await _userManager.GetAccessFailedCountAsync(user);

                        ModelState.AddModelError("", $"{failCount} kez başarısız giriş");

                        if (failCount == 3)
                        {
                            await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(20)));

                            ModelState.AddModelError("", "Hesabınız 3 başarısız girişten dolayı 20 dakika süreyle kitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Email adresi veya şifreniz geçersiz.");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Bu mail adresine kayıtlı kullanıcı bulunamamıştır.");
                }
            }

            return View(userLogin);

        }
    }
}
