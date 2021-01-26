using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ScratchPad.Identity
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        // store any number of tables along with default tables.
        public ApplicationDbContext():base("defaultConnection")
        {
            
        }
    }
}