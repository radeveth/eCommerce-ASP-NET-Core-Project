namespace Ecommerce.Data.Seeder
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Products.Any())
            {
                return;
            }

            string randomAdministrationRoleId = await dbContext.Roles.Where(r => r.Name == "Administartor").Select(r => r.Id).FirstOrDefaultAsync();
            string randomUserWhoIsAdmin = await dbContext.UserRoles.Where(u => u.RoleId == randomAdministrationRoleId).Select(u => u.UserId).FirstOrDefaultAsync();

            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Name = "Smartphone Apple iPhone 14",
                    Price = 1999,
                    Status = Status.Available,
                    Quantity = 5,
                    Description = "random phone description that is more than 10 characters.",
                    BrandId = dbContext.Brands.FirstOrDefault().Id, // TODO
                    UserId = randomUserWhoIsAdmin,
                },
                new Product()
                {
                    Name = "Dryer Bosch WTH85203BY",
                    Price = 1129,
                    Status = Status.Available,
                    Quantity = 200,
                    Description = "random bosch drayer description that is more than 10 characters.",
                    BrandId = dbContext.Brands.FirstOrDefault().Id, // TODO
                    UserId = randomUserWhoIsAdmin, // dbContext.ApplicationUserRoles.Select(a => a.UserId).FirstOrDefault(),
                },
                new Product()
                {
                    Name = "Console Sony Playstation 4 SLIM",
                    Price = 1145,
                    Status = Status.Available,
                    Quantity = 40,
                    Description = "random playstation description that is more than 10 characters.",
                    BrandId = dbContext.Brands.FirstOrDefault().Id, // TODO
                    UserId = randomUserWhoIsAdmin,
                },
                new Product()
                {
                    Name = "Monitor Gaming Huawei Mateview GT",
                    Price = 900,
                    Status = Status.Available,
                    Quantity = 130,
                    Description = "random gaming monitor description that is more than 10 characters.",
                    BrandId = dbContext.Brands.FirstOrDefault().Id, // TODO
                    UserId = randomUserWhoIsAdmin,
                },
            };

            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();

            List<Image> images = new List<Image>()
            {
                new Image()
                {
                    Name = "iphone_1",
                    Src = File.ReadAllBytes("C:\\Users\\User\\Documents\\GitHub\\eCommerce-ASP-NET-Core-REST-API\\api-src\\RESTful-API\\Data\\eCommerceAPI.Data.Seeder\\Images\\iphone.jpg"),
                },
                new Image()
                {
                    Name = "drayer-bosch_1",
                    Src = File.ReadAllBytes("C:\\Users\\User\\Documents\\GitHub\\eCommerce-ASP-NET-Core-REST-API\\api-src\\RESTful-API\\Data\\eCommerceAPI.Data.Seeder\\Images\\drayer-bosch.jpg"),
                },
                new Image()
                {
                    Name = "playstation_1",
                    Src = File.ReadAllBytes("C:\\Users\\User\\Documents\\GitHub\\eCommerce-ASP-NET-Core-REST-API\\api-src\\RESTful-API\\Data\\eCommerceAPI.Data.Seeder\\Images\\playstation.jpg"),
                },
                new Image()
                {
                    Name = "gaming-monitor_1",
                    Src = File.ReadAllBytes("C:\\Users\\User\\Documents\\GitHub\\eCommerce-ASP-NET-Core-REST-API\\api-src\\RESTful-API\\Data\\eCommerceAPI.Data.Seeder\\Images\\gaming-monitor.jpg"),
                },
            };

            for (int i = 0; i < images.Count; i++)
            {
                images[i].ProductId = products[i].Id;
            }

            await dbContext.Images.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();
        }
    }
}
