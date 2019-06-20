using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
//Added these namespaces:
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GrandeGifts.Models;
using System.Threading.Tasks;
using GrandeGifts.Data_Access;
using GrandeGifts.Services;
using Microsoft.EntityFrameworkCore;

namespace GrandeGifts
{
    public class Startup
    {
        //Adding an IConfiguration variable (as showin in week 12 DIPS1):
        public IConfiguration _config { get; }

        //Instantiating IConfiguration in constructor (also in DIPS1 week 12):
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Added to allow API to be called from same origin.
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });


            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Address>, DataService<Address>>();
            services.AddScoped<IDataService<Hamper>, DataService<Hamper>>();
            services.AddScoped<IDataService<ShoppingCartItem>, DataService<ShoppingCartItem>>();
            services.AddScoped<IDataService<Order>, DataService<Order>>();
            services.AddScoped<IDataService<LineItem>, DataService<LineItem>>();


            services.AddDbContext<ApplicationDbContext>(options =>
            // Rather than hard-code the connection string here, we'll use the one in the config file that we've set up.
            options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));


            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = false;
            });

            // Add the services for managing authentication and authorization
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            services.AddIdentity<ApplicationUser, IdentityRole>(
                c =>
                {
                    c.User.RequireUniqueEmail = true;
                    c.Password.RequireDigit = true;
                    c.Password.RequiredLength = 6;
                    c.Password.RequireLowercase = true;
                    c.Password.RequireUppercase = true;
                    c.Password.RequireNonAlphanumeric = true;
                }
            ).AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            //services.AddDbContext<ApplicationDbContext>();
            /*
            services.AddAuthentication()
            .AddFacebook(options =>
            {
                options.AppId = _config["auth:facebook:appid"];
                options.AppSecret = _config["auth:facebook:appsecret"];
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            /*
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            */

            // Services added from tutorial:
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseCors(MyAllowSpecificOrigins);
            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();

            // Facebook Integration:
            /*
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId: "825867607812628",
                AppSecret: "2729cacb1951b2fab13e4c5054525b58"
            });
            */
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // Initialise custom roles:
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Customer" };
            IdentityResult roleResult;


            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    // Create roles and send them to the database
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create first Super User that will manage the site
            var superUser = new ApplicationUser
            {
                UserName = "adam@email.com",
                Email = "adam@email.com",
                GivenNames = "Super",
                Surname = "User"
            };

            string password = "Password1!";

            // Check to see that the user doesn't already exist:
            var _user = await userManager.FindByEmailAsync("adam@email.com");

            if (_user == null)
            {
                var createSuperUser = await userManager.CreateAsync(superUser, password);
                if (createSuperUser.Succeeded)
                {
                    // The newly created super user is assigned to the 'SuperUser' role:
                    await userManager.AddToRoleAsync(superUser, "Admin");

                    // Later: Add dummy address to user's profile.
                }
            }
        }
    }
}
