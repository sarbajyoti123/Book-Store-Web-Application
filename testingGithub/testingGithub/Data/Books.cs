using System;
using System.Collections.Generic;

namespace testingGithub.Data
{
    public class Books
    {


        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int TotalPages { get; set; }

        public string Language { get; set; }


        public string CoverImageUrl { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public string PDFUrl { get; set; }

        public ICollection<BookGalery> BookGalery { get; set; }

        public string Useridbook { get; set; }
        

    }
}
