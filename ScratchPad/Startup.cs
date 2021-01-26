using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ScratchPad.Identity;

[assembly: OwinStartup(typeof(ScratchPad.Startup))]

namespace ScratchPad
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });

            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            //In order to work with Roles, You need to call RoleManager and in order to work with User , you need to call Usermanager
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var appDbContext = new ApplicationDbContext();
            var appUserStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);

            /*
             * ********************************
             * Create Roles
             ********************************
             */
            //create Admin role
            if (!rolemanager.RoleExists("Admin"))
            {
                var role = new IdentityRole() { Name = "Admin" };
                rolemanager.Create(role);
            }
            //create Manager role
            if (!rolemanager.RoleExists("Manager"))
            {
                var role = new IdentityRole() { Name = "Manager" };
                rolemanager.Create(role);
            }
            //create Customer role
            if (!rolemanager.RoleExists("Customer"))
            {
                var role = new IdentityRole() { Name = "Customer" };
                rolemanager.Create(role);
            }

            /*
             * ********************************
             * Create users
             ********************************
             */
            if (userManager.FindByName("Admin") == null)
            {
                var user = new ApplicationUser() { UserName = "Admin", Email = "admin@gmail.com" };
                string password = "admin@1234";

                var checkUser = userManager.Create(user, password);

                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (userManager.FindByName("Manager") == null)
            {
                var user = new ApplicationUser() { UserName = "Manager", Email = "manager@gmail.com" };
                string password = "manager@1234";

                var checkUser = userManager.Create(user, password);

                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Manager");
                }
            }



        }
    }
}
