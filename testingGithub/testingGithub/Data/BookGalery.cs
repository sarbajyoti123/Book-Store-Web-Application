namespace testingGithub.Data
{
    public class BookGalery
    {
        public int Id { get; set; }
        
        public int BookId { get; set; }
        public string Name { get; set; }

        public string URL { get; set; }

        public Books books { get; set; }


    }
}
