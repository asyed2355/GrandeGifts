using System;
using System.Threading.Tasks;
// Added namespaces:
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GrandeGifts.Models;
using GrandeGifts.Data_Access;

namespace GrandeGifts.Services
{
    public class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<ApplicationUser> uM = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<IdentityRole> rM = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                IDataService<Category> _catService = scope.ServiceProvider.GetRequiredService<IDataService<Category>>();
                IDataService<Hamper> _hamperService = scope.ServiceProvider.GetRequiredService<IDataService<Hamper>>();

                // Add Customer role:
                if (await rM.FindByNameAsync("Customer") != null)
                {
                    await rM.CreateAsync(new IdentityRole("Customer"));
                }

                // Add SuperUser role:
                if (await rM.FindByNameAsync("Admin") != null)
                {
                    await rM.CreateAsync(new IdentityRole("Admin"));
                }

                // Add first SuperUser
                if (await uM.FindByNameAsync("Admin") != null)
                {
                    ApplicationUser firstAdmin = new ApplicationUser();
                    firstAdmin.Email = "adam@email.com";
                    await uM.CreateAsync(firstAdmin, "Password1!");
                    await uM.AddToRoleAsync(firstAdmin, "Admin");
                }

                /*
                // Add first categories:
                if (_catService.Query(x => x.CategoryName == "Christmas")  )
                {
                    Category cat = new Category
                    {
                        CategoryId = 1,
                        CategoryName = "Christmas",
                        Description = "Celebrate the Christmas spirit by getting your hands on one of our amazing Christmas hampers!!",
                        ImageUrl = "c.tadst.com/gfx/750w/christmas.jpg",
                        InUse = true
                    };
                    

                    ApplicationUser firstAdmin = new ApplicationUser();
                    firstAdmin.Email = "adam@email.com";
                    await uM.CreateAsync(firstAdmin, "Password1!");
                    await uM.AddToRoleAsync(firstAdmin, "Admin");
                    */
            }
        }
    }
}
