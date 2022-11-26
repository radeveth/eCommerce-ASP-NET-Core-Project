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

            string randomAdministrationRoleId = await dbContext.Roles.Where(r => r.Name == "Administartor").Select(r => r.Id).FirstOrDefaultAsync();
            string randomUserWhoIsAdmin = await dbContext.UserRoles.Where(u => u.RoleId == randomAdministrationRoleId).Select(u => u.UserId).FirstOrDefaultAsync();

            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Smartphones",
                    Description = "",
                    UserId = randomUserWhoIsAdmin,
                },
                new Category()
                {
                    Name = "Electric Appliances",
                    Description = "",
                    UserId = randomUserWhoIsAdmin,
                },
                new Category()
                {
                    Name = "Fashion",
                    Description = "",
                    UserId = randomUserWhoIsAdmin,
                },
                new Category()
                {
                    Name = "Sports",
                    Description = "",
                    UserId = randomUserWhoIsAdmin,
                },
                new Category()
                {
                    Name = "Gaming",
                    Description = "",
                    UserId = randomUserWhoIsAdmin,
                },
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }
    }
}
