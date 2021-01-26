using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScratchPad.ViewModels
{
    public class RegistrationViewModels
    {
        [Required(ErrorMessage ="username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; }
        [Required(ErrorMessage="Confirm password cannot be blank")]
        [Compare(otherProperty:"Password",ErrorMessage = "Password and Confirm password must be same")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Email adress must be valid")]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
    }
}