using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using testingGithub.CutomValidationInputs;
using testingGithub.Data;
using testingGithub.Models;
using testingGithub.Repository;
using testingGithub.Service;

namespace testingGithub
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BookStoreContext>();


            services.AddControllersWithViews();

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();

            services.AddScoped<SignupRepo, SignupRepo>();

            services.AddScoped<ContactRepo, ContactRepo>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddScoped<UserService, UserService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 3;

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(10);
            });

            services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig"));
            services.AddScoped<EmailService, EmailService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"MyImages")),
                RequestPath = "/MyImages"
            });


            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {

                    //await context.Response.WriteAsync(env.EnvironmentName);
                    await context.Response.WriteAsync("Hello Sarbajyotiiii");


                });
            });

            */



            /*

            //Custom  Middleware
            //app.use()

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello First Request");

                await next();

                await context.Response.WriteAsync("Hello got from first response");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello Second Request");

                await next();

                await context.Response.WriteAsync("Hello got from Second response");
            });


            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello Third Request");

                
            });


            */


            /*

            //app.Map

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async (context) =>
                {
                    await context.Response.WriteAsync("Hello first");
                });
            });

            */


            /*

                     if (env.IsDevelopment())
                     {
                         await context.Response.WriteAsync("dev");

                     }
                     else if (env.IsProduction())
                     {
                         await context.Response.WriteAsync("Prod");
                     }

                     else if (env.IsEnvironment("devprod"))
                     {
                         await context.Response.WriteAsync("Custom type");
                     }

                    */






            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                 {


                     

                     //await context.Response.WriteAsync(env.EnvironmentName);
                     await context.Response.WriteAsync("Hello Sarbajyotiiii");

                     
                 });
            });

           
            */






            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            */


        }
    }
}
