using System.IO;
using System.Net;
using crowsoftmvc.Areas.Identity.Data;
using crowsoftmvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace crowsoftmvc
{
    /// <summary>
    /// Last Updated by: Charles Aylward
    /// This is the startup class that adds configuration services and security to the ASP.NET MVC application 
    /// </summary>
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));

            // This is to add the Identity components to the application,  AddRoles adds the role components
            services.AddDefaultIdentity<CrowsoftUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImageFiles")));

            services.AddMvc(obj =>
               {
                   // This code adds Policy components to the application, to be able to the Authorize users against specific policies
                   var policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
               }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            /// <summary>
            /// Policies were added to control who can access the controllers
            /// 1. RequireAdminOnly = User with Admin Roles Only
            /// 2. AllUsers = All user roles, Admin, Client and User
            /// 3. RequireUserandAdminOnly = CrowSoft Users only, Admin and User
            /// 
            /// Note the following code require on the top of the controller.             
            /// E.g. [Authorize(Policy = "RequireAdminOnly")]
            ///      public class YourController : Controller
            /// </summary>
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminOnly", policy =>
                       policy.RequireRole("Admin"));
                options.AddPolicy("AllUsers", policy =>
                       policy.RequireRole("Admin", "Client", "User"));
                options.AddPolicy("RequireUserandAdminOnly", policy =>
                        policy.RequireRole("Admin", "User"));
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("172.28.25.133"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseStaticFiles();

            //REMEMBER TO UNCOMMENT THIS BEFORE CHECKING INTO CROWSOFT
            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
