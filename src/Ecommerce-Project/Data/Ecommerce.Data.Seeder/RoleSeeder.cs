namespace Ecommerce.Data.Seeder
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class RoleSeeder : ISeeder
    {
        public async Task SeedAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider)
        {
            await this.SeedAdministartionRoleAsync(dbContext, serviceProvider);

            await this.SeedAdministratorAsync(dbContext, serviceProvider);
        }

        private async Task SeedAdministartionRoleAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider)
        {
            RoleManager<ApplicationRole> roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (await roleManager.FindByNameAsync("Administrator") != null)
            {
                return;
            }

            IdentityResult? result = await roleManager.CreateAsync(new ApplicationRole("Administrator"));

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Error while seeding administartion role!");
            }

            await dbContext.SaveChangesAsync();
        }

        private async Task SeedAdministratorAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider)
        {
            RoleManager<ApplicationRole> roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            ApplicationRole role = await roleManager.FindByNameAsync("Administrator");

            if (role == null)
            {
                await this.SeedAdministartionRoleAsync(dbContext, serviceProvider);
            }

            if (dbContext.UserRoles.Any(u => u.RoleId == role.Id))
            {
                return;
            }

            const string adminEmail = "eshopparkadministartion@gmail.com";
            const string username = "RadevEshopParkAdministrator";
            const string adminFullName = "Radev Administartor";
            const string adminPassword = "!RrdEshopParkAdministartion!631307()!";

            ApplicationUser user = new ApplicationUser()
            {
                Email = adminEmail,
                UserName = username,
                FullName = adminFullName,
            };

            IdentityResult result = await userManager.CreateAsync(user, adminPassword);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Error while seeding administartor!");
            }

            await userManager.AddToRoleAsync(user, "Administrator");
            await dbContext.SaveChangesAsync();
        }
    }
}
