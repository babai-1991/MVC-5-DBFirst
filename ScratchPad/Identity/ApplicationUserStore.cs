using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ScratchPad.Identity
{
    /*contains methods for manipulating the users information from the database.
     It directly interact with ApplicationDbContext internally.*/
    public class ApplicationUserStore:UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
    }
}