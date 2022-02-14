using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using testingGithub.Models;
using testingGithub.Service;

namespace testingGithub.Repository
{
    public class SignupRepo
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserService _userservice;

        private readonly EmailService _emailService;

        private readonly IConfiguration _configuration;

        public SignupRepo(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,UserService userService,EmailService emailService,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userservice = userService;
            _emailService = emailService;
            _configuration = configuration;


        }


        public async Task<IdentityResult> SignUp(SignUpUserModel model)
        {
            var data = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DOB = model.DOB,
                Email = model.Email,
                UserName = model.Email

            };
            
            var cred = await _userManager.CreateAsync(data, model.Password);
            if (cred.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(data);
                if (!string.IsNullOrEmpty(token))
                {
                    await SendEmailConfirmation(data, token);
                }
            }
            return cred;
        }


        public async Task<SignInResult> PasswordSignInAsync(LoginModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,true);
            
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<IdentityResult> ForgetPasswordAsync(ForgetPasswordModel model)
        {
            var userid = _userservice.GetUserId();
            var user = await _userManager.FindByIdAsync(userid);
            var data = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return data;

        }

        public async Task SendEmailConfirmation(ApplicationUser user,string token)
        {
            string appdomain = _configuration.GetSection("Application:Domain").Value;
            string emailconfirmationurl = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                Placeholder = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.FirstName),
                    new KeyValuePair<string, string>("{{URL}}",string.Format(appdomain+emailconfirmationurl,user.Id,token))
                }
            };
            await _emailService.ConfirmationEmail(options);
        }

        public async Task<IdentityResult> ConfirmEmailUser(string uid,string token)
        {

            var data =  await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
            return data;
        }


        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task ForgetPasswordTokenGenerate(ApplicationUser user)
        {
            var token = await  _userManager.GeneratePasswordResetTokenAsync(user);
            if(!string.IsNullOrEmpty(token))
            {
                await SendForgetPasswordConfirmation(user, token);
            }
        }


        public async Task SendForgetPasswordConfirmation(ApplicationUser user, string token)
        {
            string appdomain = _configuration.GetSection("Application:Domain").Value;
            string emailconfirmationurl = _configuration.GetSection("Application:ForgetPasswordConfirmation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                Placeholder = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.FirstName),
                    new KeyValuePair<string, string>("{{URL}}",string.Format(appdomain+emailconfirmationurl,user.Id,token))
                }
            };
            await _emailService.ForgetPasswordEmail(options);
        }



        public async Task<IdentityResult> ResetPassworddAsync(ResetPasswordModel model)
        {
            var data = await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.Userid), model.Token, model.NewPassword);
            return data;
        }







    }
}
