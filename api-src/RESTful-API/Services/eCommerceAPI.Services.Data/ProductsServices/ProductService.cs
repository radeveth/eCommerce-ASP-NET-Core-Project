namespace eCommerceAPI.Services.Data.ProductsServices
{
    using AutoMapper;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.Products;
    using eCommerceAPI.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly EcommerceApiDbContext dbContext;

        public ProductService(EcommerceApiDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            Product? product = await this.dbContext
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);

            return this.mapper.Map<ProductViewModel>(product);
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return this.mapper.Map<IEnumerable<ProductViewModel>>(this.dbContext.Products);
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
                    CategoryId = category,
                };

                await this.dbContext.ProductCategories.AddAsync(productCategory);
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
