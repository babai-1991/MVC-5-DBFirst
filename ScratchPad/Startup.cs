using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(ScratchPad.Startup))]

namespace ScratchPad
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                //This means we are going to store the user's identity details inside a cookie temporarily when he login.
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //If not logged in , redirects to Account/Login URL
                LoginPath = new PathString("~/Account/Login")
            });
        }
    }
}
