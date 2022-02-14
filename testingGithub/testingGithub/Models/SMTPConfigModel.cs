namespace testingGithub.Models
{
    public class SMTPConfigModel
    {

        public string SenderAddress { get; set; }

        public string SenderDisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public bool EnableSSL { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public bool IsBodyHTML { get; set; }



    }
}
