﻿using ASP.NetCoreIdentity.Models;
using ASP.NetCoreIdentity.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : BaseController
    {

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(userManager, null, roleManager)
        {

        }
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public IActionResult Claims()
        {
            return View(User.Claims.ToList());
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RoleCreate(RoleViewModel roleViewModel)
        {

            AppRole role = new AppRole();
            role.Name = roleViewModel.Name;

            IdentityResult result = _roleManager.CreateAsync(role).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                AddModelError(result);
            }

            return View(roleViewModel);
        }

        public IActionResult RoleDelete(string id)
        {
            AppRole role = new AppRole();
            role = _roleManager.FindByIdAsync(id).Result;

            if (role != null)
            {
                IdentityResult result = _roleManager.DeleteAsync(role).Result;
            }

            return RedirectToAction("Roles");

        }

        public IActionResult RoleUpdate(string id)
        {
            AppRole role = new AppRole();
            role = _roleManager.FindByIdAsync(id).Result;

            return View(role.Adapt<RoleViewModel>());

        }

        [HttpPost]
        public IActionResult RoleUpdate(RoleViewModel roleViewModel)
        {
            AppRole role = _roleManager.FindByIdAsync(roleViewModel.Id).Result;

            if (role != null)
            {
                role.Name = roleViewModel.Name;
                IdentityResult result = _roleManager.UpdateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    AddModelError(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Güncelleme İşlemi Başarısız Oldu!");
            }

            return View(roleViewModel);

        }

        public IActionResult Roles()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Users()
        {

            return View(_userManager.Users.ToList());
        }

        public IActionResult RoleAssign(string id)
        {
            TempData["UserId"] = id;

            AppUser user = _userManager.FindByIdAsync(id).Result;

            ViewBag.userName = user.UserName;

            IQueryable<AppRole> roles=_roleManager.Roles;

            IList<string> userRoles = _userManager.GetRolesAsync(user).Result as List<string>;

            List<RoleAssignViewModel> roleAssignViewModels = new List<RoleAssignViewModel>();

            foreach (var role in roles)
            {
                RoleAssignViewModel r = new RoleAssignViewModel();
                r.RoleId = role.Id;
                r.RoleName = role.Name;

                if (userRoles.Contains(role.Name))
                {
                    r.Exist = true;
                }
                else
                {
                    r.Exist = false;
                }

                roleAssignViewModels.Add(r);
            }



            return View(roleAssignViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> roleAssignViewModels)
        {
            AppUser user = _userManager.FindByIdAsync(TempData["UserId"].ToString()).Result;

            foreach (var item in roleAssignViewModels)
            {
                if (item.Exist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }

            return RedirectToAction("Users");




        }
    }
}
