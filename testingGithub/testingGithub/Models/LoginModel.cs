using System.ComponentModel.DataAnnotations;

namespace testingGithub.Models
{
    public class LoginModel
    {

        public int ID { get; set; }

        [Required(ErrorMessage ="Incorrect Email ID")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Incorrect Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }



    }
}
