namespace Ecommerce.Services.Data.ProductsServices
{
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Products;
    using Ecommerce.ViewModels.Products;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly EcommerceDbContext dbContext;

        public ProductService(EcommerceDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            Product product = await this.dbContext
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

            try
            {
                int imagesCounter = 0;
                foreach (var image in productForm.Images)
                {
                    IFormFile imageFile = image.Src;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        imagesCounter++;
                        using var stream = new MemoryStream();
                        await imageFile.CopyToAsync(stream);

                        Image newImage = new Image()
                        {
                            Name = $"{product.Name.Replace(" ", "-")}_{imagesCounter}",
                            Src = stream.ToArray(),
                            ProductId = product.Id,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

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
