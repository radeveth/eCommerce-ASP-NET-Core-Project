namespace eCommerceAPI.Data.Seeder
{
    using System;
    using System.Threading.Tasks;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(EcommerceApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Smartphones",
                    Description = "",
                    UserId = "1044379359483265024",// dbContext.ApplicationUserRoles.Select(a => a.UserId).FirstOrDefault()
                },
                new Category()
                {
                    Name = "Electric Appliances",
                    Description = "",
                    UserId = "1044379359483265024",
                },
                new Category()
                {
                    Name = "Fashion",
                    Description = "",
                    UserId = "1044379359483265024",
                },
                new Category()
                {
                    Name = "Sports",
                    Description = "",
                    UserId = "1044379359483265024",
                },
                new Category()
                {
                    Name = "Gaming",
                    Description = "",
                    UserId = "1044379359483265024",
                },
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }
    }
}
