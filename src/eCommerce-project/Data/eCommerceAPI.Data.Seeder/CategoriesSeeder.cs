namespace eCommerce.Data.Seeder
{
    using System;
    using System.Threading.Tasks;
    using eCommerce.Data;
    using eCommerce.Data.Models;

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
                    UserId = dbContext.ApplicationUsers.FirstOrDefault().Id,
                },
                new Category()
                {
                    Name = "Electric Appliances",
                    Description = "",
                    UserId = dbContext.ApplicationUsers.FirstOrDefault().Id,
                },
                new Category()
                {
                    Name = "Fashion",
                    Description = "",
                    UserId = dbContext.ApplicationUsers.FirstOrDefault().Id,
                },
                new Category()
                {
                    Name = "Sports",
                    Description = "",
                    UserId = dbContext.ApplicationUsers.FirstOrDefault().Id,
                },
                new Category()
                {
                    Name = "Gaming",
                    Description = "",
                    UserId = dbContext.ApplicationUsers.FirstOrDefault().Id,
                },
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }
    }
}
