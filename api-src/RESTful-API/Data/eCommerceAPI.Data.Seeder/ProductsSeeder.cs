namespace eCommerceAPI.Data.Seeder
{
    using System;
    using System.Threading.Tasks;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.Data.Models.Enums;
    using eCommerceAPI.InputModels.Products;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(EcommerceApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            List<ProductFormModel> products = new List<ProductFormModel>()
            {
                new ProductFormModel()
                {
                    Name = "Smartphone Apple iPhone 14",
                    Price = 1999,
                    Status = Status.Available,
                    Quantity = 5,
                    Description = "random phone description that is more than 10 characters.",
                    Images = new List<ProductImageInputModel>()
                    {
                        new ProductImageInputModel()
                        {
                            Src = GetFormFile("iphone.jpg"),
                        },
                    },
                    BrandId = 1, // TODO
                    UserId = "1044379359483265024",
                },
                new ProductFormModel()
                {
                    Name = "Dryer Bosch WTH85203BY",
                    Price = 1129,
                    Status = Status.Available,
                    Quantity = 200,
                    Description = "random bosch drayer description that is more than 10 characters.",
                    Images = new List<ProductImageInputModel>()
                    {
                        new ProductImageInputModel()
                        {
                            Src = GetFormFile("drayer-bosch.jpg"),
                        },
                    },
                    BrandId = 1, // TODO
                    UserId = "1044379359483265024",
                },
                new ProductFormModel()
                {
                    Name = "Console Sony Playstation 4 SLIM",
                    Price = 1145,
                    Status = Status.Available,
                    Quantity = 40,
                    Description = "random playstation description that is more than 10 characters.",
                    Images = new List<ProductImageInputModel>()
                    {
                        new ProductImageInputModel()
                        {
                            Src = GetFormFile("playstation.jpg"),
                        },
                    },
                    BrandId = 1, // TODO
                    UserId = "1044379359483265024",
                },
                new ProductFormModel()
                {
                    Name = "Monitor Gaming Huawei Mateview GT",
                    Price = 900,
                    Status = Status.Available,
                    Quantity = 130,
                    Description = "random gaming monitor description that is more than 10 characters.",
                    Images = new List<ProductImageInputModel>()
                    {
                        new ProductImageInputModel()
                        {
                            Src = GetFormFile("gaming-monitor.jpg"),
                        },
                    },
                    BrandId = 1, // TODO
                    UserId = "1044379359483265024",
                },
            };

            // TODO -> service

            throw new NotImplementedException();
        }

        private static IFormFile GetFormFile(string path)
        {
            FormFile file;
            using (var stream = File.OpenRead(path))
            {
                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            }

            return file;
        }
    }
}
