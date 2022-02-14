using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using testingGithub.Data;

namespace testingGithub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BookStoreContext>();

            services.AddControllersWithViews();
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

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
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
