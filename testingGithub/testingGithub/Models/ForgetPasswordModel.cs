using System.ComponentModel.DataAnnotations;

namespace testingGithub.Models
{
    public class ForgetPasswordModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please Enter New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Re-Enter New Password")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }




    }
}
