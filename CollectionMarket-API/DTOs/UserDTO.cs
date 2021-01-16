using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
         public string Name { get; set; }
    }

    public class UserProfileDTO
    {
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string PostCode { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string Address { get; set; }
    }

    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterDTO
    {
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "This field must have from 7 to 20 characters.", MinimumLength = 7)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "This field must have from 7 to 20 characters.", MinimumLength = 7)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string Email { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string LastName { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string City { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string PostCode { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "This field can be up to 256 characters.")]
        public string Address { get; set; }
    }

}
