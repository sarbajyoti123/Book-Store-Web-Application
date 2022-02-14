using System.ComponentModel.DataAnnotations;

namespace testingGithub.CutomValidationInputs
{
    public class InputValidationAttribute: ValidationAttribute
    {

        public string Text { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                string val = value.ToString();
                if (val.Contains(Text))
                {
                    return ValidationResult.Success;
                }
            }
            


            return new ValidationResult(ErrorMessage ?? "Bookname does not contain desired value");
        }




    }
}
