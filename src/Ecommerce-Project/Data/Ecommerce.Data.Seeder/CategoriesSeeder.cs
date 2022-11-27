namespace Ecommerce.Data.Seeder
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            RoleManager<ApplicationRole> roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            ApplicationRole role = await roleManager.FindByNameAsync("Administrator");

            string userId = await dbContext.UserRoles.Where(u => u.RoleId == role.Id).Select(u => u.UserId).FirstOrDefaultAsync();

            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Smartphones",
                    Description = "",
                    UserId = userId,
                },
                new Category()
                {
                    Name = "Electric Appliances",
                    Description = "",
                    UserId = userId,
                },
                new Category()
                {
                    Name = "Fashion",
                    Description = "",
                    UserId = userId,
                },
                new Category()
                {
                    Name = "Sports",
                    Description = "",
                    UserId = userId,
                },
                new Category()
                {
                    Name = "Gaming",
                    Description = "",
                    UserId = userId,
                },
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }
    }
}
