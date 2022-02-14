using Microsoft.AspNetCore.Identity;
using System;

namespace testingGithub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }



    }
}
