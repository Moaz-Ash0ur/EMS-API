using EMS.DAL.Data;
using EMS.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace EMS_API.Helper
{
    public class ContextConfig
    {
        private static readonly string seedAdminEmail = "admin@gmail.com";
        public static async Task SeedDataAsync(AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedUserAsync(userManager, roleManager);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Ensure roles exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Ensure admin user exists
            var adminEmail = seedAdminEmail;
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var id = Guid.NewGuid().ToString();
                adminUser = new ApplicationUser
                {
                    Id = id,
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "admin",
                    LastName = "admin",
                    PhoneNumber = "01011121314"
                };
                var result = await userManager.CreateAsync(adminUser, "admin123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }


}


