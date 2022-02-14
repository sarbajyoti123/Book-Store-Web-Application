using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using testingGithub.Models;
using testingGithub.Repository;
using testingGithub.Service;
//using Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signinuser;

namespace testingGithub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // IConfiguration is for to fetch data from appsettings.json file or from its Dev version or Production
        private readonly IConfiguration _iconfiguration;


        private readonly UserService _userService;

        private readonly EmailService _emailService;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ContactRepo _contactRepo;
        public HomeController(ILogger<HomeController> logger,IConfiguration configuration,UserService userService, EmailService emailService, SignInManager<ApplicationUser> signInManager,ContactRepo contactRepo)
        {
            _logger = logger;
            _iconfiguration = configuration;
            _userService = userService;
            _emailService = emailService;
            _signInManager = signInManager;
            _contactRepo = contactRepo;

        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var data = User.FindFirst("UserFirstName");
                
                var useremail = User.FindFirst("UserEmailbook");
                string email = useremail.Value;
                var data2 = data.Value;
                UserEmailOptions options = new UserEmailOptions
                {
                    ToEmails = new List<string>() { email },
                    Placeholder = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",data2)
                }
                };
                await _emailService.SendTestEmail(options);
            }




            //Fetching data from appsettings.json file
            //var data = _iconfiguration["MyName"];
            //var data2 = _iconfiguration["Family:Father"];
            //var data3 = _iconfiguration["Family:Mother"];
            //var data4 = _iconfiguration["Family:Brother:Son"];
            //ViewBag.val = data4;

            var UserId = _userService.GetUserId();

            bool isLoggedIn = _userService.IsAuthenticated();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  ContactUs(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                int id = await _contactRepo.ContactFormUser(model);
                if(id > 0)
                {
                    return RedirectToAction("ContactUs");
                }
            }
            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
