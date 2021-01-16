using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    public class RegistrationModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "This field must have from 7 to 20 characters.", MinimumLength = 7)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "This field must have from 7 to 20 characters.", MinimumLength = 7)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
    public class UserProfileModel
    {

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }

    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
