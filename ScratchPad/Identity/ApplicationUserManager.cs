using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ScratchPad.Identity
{
    /* contains methods that are used for manipulating
     user's information such GetEmail(),SendEmail(),GetRole() etc */
    public class ApplicationUserManager:UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }
    }
}