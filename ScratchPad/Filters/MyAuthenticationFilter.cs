using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace ScratchPad.Filters
{
    public class MyAuthenticationFilter:FilterAttribute,IAuthenticationFilter
    {
        //Will execute automatically by MVC
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //get current user details
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }


        //This method will execute after OnAuthentication()
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}