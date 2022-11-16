namespace eCommerceAPI.Services.Data.ProductsService
{
    using System.Threading.Tasks;
    using AutoMapper;
    using eCommerceAPI.Data;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.Products;
    using eCommerceAPI.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly EcommerceApiDbContext dbContext;
        private readonly IMapper mapper;

        public ProductService(EcommerceApiDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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

        public async Task<T> GetViewModelById<T>(int id)
        {
            Product? product = await this.dbContext
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);

            return this.mapper.Map<T>(product);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return this.mapper
                .Map<IEnumerable<ProductViewModel>>(this.dbContext.Products);
        }
    }
}
