namespace Ecommerce.Data.Seeder
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;

    public class BrandsSeeder : ISeeder
    {
        public async Task SeedAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Brands.Any())
            {
                return;
            }

            List<Brand> brands = new List<Brand>()
            {
                new Brand()
                {
                    Name = "Sony",
                    Description = string.Empty,
                    YearOfFoundation = 1946,
                    FounderName = "Masaru Ibuka",
                },
            };

            await dbContext.Brands.AddRangeAsync(brands);
            await dbContext.SaveChangesAsync();
        }
    }
}
