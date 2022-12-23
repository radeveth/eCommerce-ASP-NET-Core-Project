namespace Ecommerce.Data.Seeder
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DataSeeder : ISeeder
    {
        public async Task SeedAsync(EcommerceDbContext dbContext, IServiceProvider serviceProvider)
        {
            RoleManager<ApplicationRole> roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            ApplicationRole role = await roleManager.FindByNameAsync("Administrator");

            string userId = await dbContext.UserRoles.Where(u => u.RoleId == role.Id).Select(u => u.UserId).FirstOrDefaultAsync();
            ApplicationUser user = await userManager.FindByIdAsync(userId);

            // Brand Seeder
            List<Brand> brands = new List<Brand>()
            {
                new Brand()
                {
                    Name = "Sony",
                    Description = "Sony, in full Sony Corporation, major Japanese manufacturer of consumer electronics products. It also was involved in films, music, and financial services, among other ventures.",
                    YearOfFoundation = 1946,
                    FounderName = "Masaru Ibuka",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Brand()
                {
                    Name = "FIFA",
                    Description = "FIFA is the official international governing body of football. The Company's mission is to organise the World Cup and the various other world cup competitions, safeguarding the Laws of the Game, developing the game around the world and to bringing hope to those less privileged.",
                    YearOfFoundation = 1904,
                    FounderName = "Frenchman Robert Guerin",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Brand()
                {
                    Name = "Apple",
                    Description = "Apple Inc (Apple) designs, manufactures, and markets smartphones, tablets, personal computers (PCs), portable and wearable devices. The company also offers software and related services, accessories, and third-party digital content and applications.",
                    YearOfFoundation = 1976,
                    FounderName = "Steve Jobs",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Brand()
                {
                    Name = "Bosch",
                    Description = "The Bosch Group is a leading global supplier of technology and services. It employs roughly 402,600 associates worldwide (as of December 31, 2021). The company generated sales of 78.7 billion euros in 2021.",
                    YearOfFoundation = 1886,
                    FounderName = "Robert Bosch",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Brand()
                {
                    Name = "Huawei",
                    Description = "Huawei is a Chinese information and communications technology (ICT) company that specializes in telecommunications equipment. The company also offers services and consumer electronics including wearables, mobile broadband modems, smartphones, tablets and PCs.",
                    YearOfFoundation = 1987,
                    FounderName = "Ren Zhengfei",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };
            await this.SeedBrands(dbContext, brands);

            // Categories Seeder
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Smartphones",
                    Description = string.Empty,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Category()
                {
                    Name = "Electric Appliances",
                    Description = string.Empty,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Category()
                {
                    Name = "Fashion",
                    Description = string.Empty,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Category()
                {
                    Name = "Sports",
                    Description = string.Empty,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Category()
                {
                    Name = "Gaming",
                    Description = string.Empty,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };
            foreach (var category in categories)
            {
                category.UserId = userId;
                category.User = user;
            }
            await this.SeedCategories(dbContext, categories);

            // Products Seeder
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Name = "Smartphone Apple iPhone 14",
                    Price = 1999,
                    Status = Status.Available,
                    Quantity = 5,
                    Description = "random phone description that is more than 10 characters.",
                    BrandId = brands.FirstOrDefault(b => b.Name == "Apple").Id,
                    Brand = brands.FirstOrDefault(b => b.Name == "Apple"),
                    CategoryId = categories.FirstOrDefault(c => c.Name == "Smartphones").Id,
                    Category = categories.FirstOrDefault(c => c.Name == "Smartphones"),
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Product()
                {
                    Name = "Dryer Bosch WTH85203BY",
                    Price = 1129,
                    Status = Status.Available,
                    Quantity = 200,
                    Description = "random bosch drayer description that is more than 10 characters.",
                    BrandId = brands.FirstOrDefault(b => b.Name == "Bosch").Id,
                    Brand = brands.FirstOrDefault(b => b.Name == "Bosch"),
                    CategoryId = categories.FirstOrDefault(c => c.Name == "Electric Appliances").Id,
                    Category = categories.FirstOrDefault(c => c.Name == "Electric Appliances"),
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Product()
                {
                    Name = "Console Sony Playstation 4 SLIM",
                    Price = 1145,
                    Status = Status.Available,
                    Quantity = 40,
                    Description = "random playstation description that is more than 10 characters.",
                    BrandId = brands.FirstOrDefault(b => b.Name == "Sony").Id,
                    Brand = brands.FirstOrDefault(b => b.Name == "Sony"),
                    CategoryId = categories.FirstOrDefault(c => c.Name == "Gaming").Id,
                    Category = categories.FirstOrDefault(c => c.Name == "Gaming"),
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Product()
                {
                    Name = "Monitor Gaming Huawei Mateview GT",
                    Price = 900,
                    Status = Status.Available,
                    Quantity = 130,
                    Description = "random gaming monitor description that is more than 10 characters.",
                    BrandId = brands.FirstOrDefault(b => b.Name == "Huawei").Id,
                    Brand = brands.FirstOrDefault(b => b.Name == "Huawei"),
                    CategoryId = categories.FirstOrDefault(c => c.Name == "Gaming").Id,
                    Category = categories.FirstOrDefault(c => c.Name == "Gaming"),
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };
            foreach (var product in products)
            {
                product.UserId = userId;
                product.User = user;
            }
            await this.SeedProducts(dbContext, products);
        }

        public async Task SeedBrands(EcommerceDbContext dbContext, List<Brand> brands)
        {
            if (dbContext.Brands.Any())
            {
                return;
            }

            await dbContext.Brands.AddRangeAsync(brands);
            await dbContext.SaveChangesAsync();
        }

        public async Task SeedCategories(EcommerceDbContext dbContext, List<Category> categories)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }

        public async Task SeedProducts(EcommerceDbContext dbContext, List<Product> products)
        {
            if (dbContext.Products.Any())
            {
                return;
            }

            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();

            List<Image> images = new List<Image>()
            {
                new Image()
                {
                    Name = "iphone_1",
                    Src = File.ReadAllBytes("C:\\Users\\User\\Documents\\GitHub\\eCommerce-ASP-NET-Core-REST-API\\src\\Ecommerce-Project\\Data\\Ecommerce.Data.Seeder\\Images\\iphone.jpg"),
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Image()
                {
                    Name = "drayer-bosch_1",
                    Src = File.ReadAllBytes("C:\\Users\\User\\Documents\\GitHub\\eCommerce-ASP-NET-Core-REST-API\\src\\Ecommerce-Project\\Data\\Ecommerce.Data.Seeder\\Images\\drayer-bosch.jpg"),
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Image()
                {
                    Name = "playstation_1",
                    Src = File.ReadAllBytes("C:\\Users\\User\\Documents\\GitHub\\eCommerce-ASP-NET-Core-REST-API\\src\\Ecommerce-Project\\Data\\Ecommerce.Data.Seeder\\Images\\playstation.jpg"),
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
                new Image()
                {
                    Name = "gaming-monitor_1",
                    Src = File.ReadAllBytes("C:\\Users\\User\\Documents\\GitHub\\eCommerce-ASP-NET-Core-REST-API\\src\\Ecommerce-Project\\Data\\Ecommerce.Data.Seeder\\Images\\gaming-monitor.jpg"),
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };

            for (int i = 0; i < images.Count; i++)
            {
                images[i].Product = products[i];
                images[i].ProductId = products[i].Id;
            }

            await dbContext.Images.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();
        }
    }
}
