using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ScratchPad.Identity
{
    /* the ApplicationUser class represent the structure of the data that you want to store
     related to Users in the Identity database
    */
    public class ApplicationUser:IdentityUser
    {
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}