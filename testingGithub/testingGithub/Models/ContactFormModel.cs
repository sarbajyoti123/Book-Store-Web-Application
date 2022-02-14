using System.ComponentModel.DataAnnotations;

namespace testingGithub.Models
{
    public class ContactFormModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Please Input Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Input Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Input Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please Input Message")]
        public string Message { get; set; }




    }
}
