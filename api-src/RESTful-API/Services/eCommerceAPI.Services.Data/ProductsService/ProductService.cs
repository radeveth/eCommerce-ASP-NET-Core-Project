namespace eCommerceAPI.Services.Data.ProductsService
{
    using System.Threading.Tasks;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.Data.Models.Enums;
    using eCommerceAPI.InputModels.Products;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class ProductService : IProductService
    {
        private readonly EcommerceApiDbContext dbContext;

        public ProductService(EcommerceApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(ProductFormModel productForm)
        {
            Product product = new Product()
            {
                Name = productForm.Name,
                Price = productForm.Price,
                Description = productForm.Description,
                Status = productForm.Status,
                Quantity = productForm.Quantity,
                BrandId = productForm.BrandId,
                UserId = productForm.UserId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            await this.dbContext.Products.AddAsync(product);
            await this.dbContext.SaveChangesAsync();

            foreach (var category in productForm.Categories)
            {
                ProductCategory productCategory = new ProductCategory()
                {
                    ProductId = product.Id,
                    CategoryId = category.Id,
                };

                await this.dbContext.ProductCategories.AddAsync(productCategory);
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
