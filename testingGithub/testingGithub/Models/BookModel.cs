using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testingGithub.CutomValidationInputs;

namespace testingGithub.Models
{
    public class BookModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 3)]

        //Regex Validation ************************************************

        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 Alphabet and 1 Number")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet and 1 Number")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}", ErrorMessage = "Password must contain: Minimum 8 and Maximum 10 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]


        //Custom Validation ************************************
        //[InputValidation(Text = "Azure")]
        public string Title { get; set; }



        //[Required]
        //[DataType(DataType.Currency)]
        //public string Date { get; set; }





        [Required]
        [StringLength(50,MinimumLength =3)]
        public string Author { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please Input Total Pages")]
        //[StringLength(50, MinimumLength = 3)]
        public int TotalPages { get; set; }

        [Required(ErrorMessage = "Please Select a Language")]
        public  string Language { get; set; }

        //public List<string> MultiLanguage { get; set; }


        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public string CoverImageUrl { get; set; }

        //[Required(ErrorMessage = "Please Upload a Cover Photo Of the Book")]
        [NotMapped]
        public IFormFile CoverPhoto { get; set; }

        [NotMapped]
        public IFormFileCollection GalleryFiles { get; set; }



        [NotMapped]
        public IFormFile PDFFile { get; set; }

        public string PDFURL { get; set; }

        public List<GalleryModel> Gallery { get; set; } 


        public string Useridbookss { get; set; }




    }
}
