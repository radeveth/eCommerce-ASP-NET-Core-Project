namespace Ecommerce.Services.Data.ProductsServices
{
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Data.Models.Enums;
    using Ecommerce.InputModels.Products;
    using Ecommerce.ViewModels.Images;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.Products.Enums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
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

        public async Task<T> GetByIdAsync<T>(int id)
        {
            Product product = await this.dbContext
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);

            return this.mapper.Map<T>(product);
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            IEnumerable<Product> products = this.dbContext.Products.AsQueryable();

            foreach (var product in products)
            {
                product.Category = this.dbContext.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
                product.Images = this.dbContext.Images.Where(i => i.ProductId == product.Id).ToList();
            }

            return this.mapper.Map<IEnumerable<ProductViewModel>>(products.AsQueryable());
        }

        public ProductFormModel GetProductFormModel()
        {
            return new ProductFormModel()
            {
                Brands = this.mapper.Map<IEnumerable<ProductBrandFormModel>>(this.dbContext.Brands),
                Categories = this.mapper.Map<IEnumerable<ProductCategoryFormModel>>(this.dbContext.Categories),
            };
        }

        public async Task CreateAsync(ProductFormModel productForm)
        {
            Product product = new Product()
            {
                Name = productForm.Name,
                Price = productForm.Price,
                Description = productForm.Description,
                Status = productForm.Quantity > 0 ? Status.Available : Status.Unavailable,
                Quantity = productForm.Quantity,
                BrandId = productForm.BrandId,
                UserId = productForm.UserId,
                CategoryId = productForm.CategoryId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            await this.dbContext.Products.AddAsync(product);
            await this.dbContext.SaveChangesAsync();

            try
            {
                List<Image> images = new List<Image>();
                int countOfImages = 0;
                foreach (var imageFile in productForm.Images)
                {
                    if (imageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);

                            // Upload the file if less than 2 MB
                            if (memoryStream.Length < 2097152)
                            {
                                Image newImage = new Image()
                                {
                                    Name = $@"{product.Name}_{++countOfImages}",
                                    Src = memoryStream.ToArray(),
                                    ProductId = product.Id,
                                    Product = product,
                                };

                                images.Add(newImage);
                            }
                        }
                    }
                }

                await this.dbContext.Images.AddRangeAsync(images);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetByAllProductsForCategory(string category)
        {
            if (category == "all")
            {
                return this.GetAll();
            }

            category = category.ToLower().Replace(" ", string.Empty);
            IEnumerable<ProductViewModel> products = this.GetAll().Where(p => p.Category.ToLower().Replace(" ", string.Empty) == category);

            return products;
        }

        public async Task<ProductsServiceModel> GetProductsServiceModel(ProductsSorting productsSorting, string category, int currentPage = 1)
        {
            IEnumerable<ProductViewModel> products = await this.GetByAllProductsForCategory(category);

            ProductsServiceModel productsServiceModel = new ProductsServiceModel()
            {
                CurrentPage = currentPage,
                SearchCategory = category == "all" ? "all" : products.Any() ? products.FirstOrDefault().Category : category,
                Products = products.Skip((currentPage - 1) * ProductsServiceModel.ProductsPerPage).Take(ProductsServiceModel.ProductsPerPage),
                TotalProducts = products.Count(),
            };

            if (products.Any())
            {
                productsServiceModel.CheapestProduct = products.OrderBy(p => p.Price).FirstOrDefault().Price;
                productsServiceModel.MostExpensiveProduct = products.OrderByDescending(p => p.Price).FirstOrDefault().Price;

                productsServiceModel.Products = productsSorting switch
                {
                    ProductsSorting.TheLowestPrice => productsServiceModel.Products.OrderBy(p => p.Price),
                    ProductsSorting.HighestPrice => productsServiceModel.Products.OrderByDescending(p => p.Price),
                    ProductsSorting.BiggestDiscount => productsServiceModel.Products.OrderByDescending(p => p.Price), // TODO
                    ProductsSorting.Latest => productsServiceModel.Products.OrderByDescending(p => p.CreatedOn),
                    ProductsSorting.Ascending => productsServiceModel.Products.OrderBy(p => p.Name),
                    ProductsSorting.Descending => productsServiceModel.Products.OrderByDescending(p => p.Name),
                    ProductsSorting.MostCommented => productsServiceModel.Products.OrderByDescending(p => p.CreatedOn), // TODO
                    _ => productsServiceModel.Products.OrderByDescending(p => p.CreatedOn),
                };
            }

            return productsServiceModel;
        }

        public async Task<ProductDetailsModel> Details(int id)
        {
            Product sourceProduct = await this.dbContext.Products.AsQueryable().FirstOrDefaultAsync(p => p.Id == id);

            ProductDetailsModel detailsModel = new ProductDetailsModel()
            {
                Id = sourceProduct.Id,
                Name = sourceProduct.Name,
                Images = this.mapper.Map<IEnumerable<ImageViewModel>>(this.dbContext.Images.Where(i => i.ProductId == id).ToList()),
                TotalReviews = sourceProduct.Reviews.Count,
                Price = sourceProduct.Price,
                Status = sourceProduct.Status,
                Description = sourceProduct.Description,
                //Category = sourceProduct.Category.Name,
                AverageReview = sourceProduct.Reviews.Count == 0 ? -1 : sourceProduct.Reviews.Sum(r => (int)r.ReviewScale) / sourceProduct.Reviews.Count,
            };

            return detailsModel;
        }
    }
}
