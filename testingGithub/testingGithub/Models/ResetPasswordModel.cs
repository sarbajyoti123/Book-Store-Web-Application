using System.ComponentModel.DataAnnotations;

namespace testingGithub.Models
{
    public class ResetPasswordModel
    {
        public int Id { get; set; }

        public string Userid { get; set; }

        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        public bool IsSuccess { get; set; }


    }
}
