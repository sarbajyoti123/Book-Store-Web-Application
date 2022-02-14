using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testingGithub.Models;
using testingGithub.Repository;

namespace testingGithub.Controllers
{
    public class SignUpController : Controller
    {

        private readonly SignupRepo _signupRepo;

        

        public SignUpController(SignupRepo signupRepo)
        {
            _signupRepo = signupRepo;
        }

        [Route("signup", Name ="signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("signup", Name = "signup")]
        [HttpPost]
        public async Task<IActionResult>  SignUp(SignUpUserModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _signupRepo.SignUp(model);
                if (!data.Succeeded)
                {
                    foreach (var error in data.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                    return View(model);

                }
            }
            ModelState.Clear();
        
            
            return View(model);
        }


        [Route("login", Name = "login")]
        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]

        [Route("login", Name = "login")]
        public async Task<IActionResult>  Login(LoginModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signupRepo.PasswordSignInAsync(model);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl)){
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "User Not Allowed");
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "User Locked Out for SomeTime for Invalid Login Attempts");
                }

                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }

                
            }
            
            return View(model);
        }

        [Route("logout",Name ="logout")]
        public async Task<IActionResult> Logout()
        {
            await _signupRepo.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [Route("forgetpasswordd", Name ="forgetpasswordd")]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [Route("forgetpasswordd", Name = "forgetpasswordd")]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signupRepo.ForgetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.isSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                
            }

            return View(model);


        }


        [Route("confirm-email")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string uid,string token)
        {
            if(!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(" ", "+");
                var result = await _signupRepo.ConfirmEmailUser(uid, token);
                if (result.Succeeded)
                {
                    ViewBag.isSuccess = true;
                }

            }
            return View();
        }

        [AllowAnonymous,HttpGet("forget")]
        public IActionResult ForgettPasswordd()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forget")]
        public async Task<IActionResult>  ForgettPasswordd(ForgetPasswordddModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _signupRepo.GetUserByEmail(model.Email);
                if (user != null)
                {
                    await _signupRepo.ForgetPasswordTokenGenerate(user);
                }

                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }

        [AllowAnonymous, HttpGet("resett-passwordd")]
        public IActionResult ResetPasswordd(string uid, string token)
        {

            ResetPasswordModel reset = new ResetPasswordModel
            {
                Userid = uid,
                Token = token
            };
            return View(reset);
        }

        [AllowAnonymous, HttpPost("resett-passwordd")]
        public async Task<IActionResult> ResetPasswordd(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ','+');
                var data = await _signupRepo.ResetPassworddAsync(model);
                if (data.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }
                foreach(var error in data.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                
            }
            return View(model);
        }


    }
}
