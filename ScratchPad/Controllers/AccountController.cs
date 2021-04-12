﻿using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ScratchPad.Identity;
using ScratchPad.ViewModels;

namespace ScratchPad.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        //[ActionName("SignUp")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModels model)
        {
            if (ModelState.IsValid)
            {
                //register user
                var appDbcontext = new ApplicationDbContext();
                var appUserStore = new ApplicationUserStore(appDbcontext);
                var appUserManager = new ApplicationUserManager(appUserStore);

                var passwordHash = Crypto.HashPassword(model.Password);

                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.Mobile,
                    PasswordHash = passwordHash,
                    City = model.City,
                    Country = model.Country,
                    Address = model.Address,
                    BirthDay = model.DateOfBirth
                };

                IdentityResult result = appUserManager.Create(user);
                if (result.Succeeded)
                {
                    //assign role
                    appUserManager.AddToRoles(user.Id, "Customer");

                    //login
                    LoginUser(appUserManager, user);
                }

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Register", "Account");
        }

        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [OverrideExceptionFilters]
        public ActionResult LogIn(LogInViewModel model)
        {
            //login
            var appdbcontext = new ApplicationDbContext();
            var appUserStore = new ApplicationUserStore(appdbcontext);
            var appusermanager = new ApplicationUserManager(appUserStore);

            var user = appusermanager.Find(model.UserName, model.Password);
            if (user != null)
            {
                LoginUser(appusermanager, user);
                if (appusermanager.IsInRole(user.Id,"Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                if (appusermanager.IsInRole(user.Id, "Manager"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Manager" });
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("error", "username/password is invalid");
            return View();

        }

        [NonAction]
        private void LoginUser(ApplicationUserManager appusermanager, ApplicationUser user)
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity =
                appusermanager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
        }

        public ActionResult LogOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyProfile()
        {
            var appdbcontext = new ApplicationDbContext();
            var appUserStore = new ApplicationUserStore(appdbcontext);
            var appusermanager = new ApplicationUserManager(appUserStore);

            ApplicationUser user = appusermanager.FindById(User.Identity.GetUserId());
            return View(user);
        }
    }
}