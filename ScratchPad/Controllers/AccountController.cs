using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
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
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity =
                        appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                }

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Register", "Account");
        }
    }
}