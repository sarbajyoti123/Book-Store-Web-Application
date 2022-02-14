using System;
using System.ComponentModel.DataAnnotations;

namespace testingGithub.Models
{
    public class SignUpUserModel
    {


        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Please Enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Please Enter Date Of Birth")]
        public DateTime DOB { get; set; }


        [Required(ErrorMessage ="Please Enter Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Enter Pasword")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }

        [Required(ErrorMessage ="Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Does not Match")]
        public string ConfirmPassword { get; set; }


    }
}
