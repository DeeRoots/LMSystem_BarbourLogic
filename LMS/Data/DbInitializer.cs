using LMS.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities;

namespace LMS.Data
{
    public class DbInitializer
    {

        public static async Task Initialize(IServiceProvider serviceProvider,
          UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> roleManager)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Users.Any())
                {
                    return;
                }

                roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_Employee)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_Member)).GetAwaiter().GetResult();

                var admin = new ApplicationUser
                {                    
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true,

                };

                await userManager.CreateAsync(admin, "!Admin173");

                var employee = new ApplicationUser
                {
                    UserName = "employee@example.com",
                    Email = "employee@example.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(employee, "!Employee173");

                var member = new ApplicationUser
                {
                    UserName = "member@example.com",
                    Email = "member@example.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(member, "!Member173");


                var applicationAdmin = userManager.Users.Where(u => u.UserName == "admin@example.com").FirstOrDefault();

                if (applicationAdmin != null)
                    userManager.AddToRoleAsync(applicationAdmin, WebSiteRole.WebSite_Admin).GetAwaiter().GetResult();

                var applicationEmployee = userManager.Users.Where(u => u.UserName == "employee@example.com").FirstOrDefault();

                if (applicationEmployee != null)
                    userManager.AddToRoleAsync(applicationEmployee, WebSiteRole.WebSite_Employee).GetAwaiter().GetResult();

                var applicationMember = userManager.Users.Where(u => u.UserName == "member@example.com").FirstOrDefault();

                if (applicationMember != null)
                    userManager.AddToRoleAsync(applicationMember, WebSiteRole.WebSite_Member).GetAwaiter().GetResult();

            }
        }
    }
}
