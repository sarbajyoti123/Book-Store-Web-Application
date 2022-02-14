using System.ComponentModel.DataAnnotations;

namespace testingGithub.Models
{
    public class ForgetPasswordddModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Input valid Email ID")]
        [EmailAddress]
        public string Email { get; set; }

        public bool EmailSent { get; set; }


    }
}
