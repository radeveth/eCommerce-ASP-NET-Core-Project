namespace Ecommerce.Services.Data.ProductsServices
{
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Data.Models.Enums;
    using Ecommerce.InputModels.Products;
    using Ecommerce.Services.Data.ApplicationUsersServices;
    using Ecommerce.ViewModels.Images;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.Products.Enums;
    using Ecommerce.ViewModels.Review;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly EcommerceDbContext dbContext;
        private readonly IApplicationUserService applicationUserService;

        public ProductService(EcommerceDbContext dbContext, IMapper mapper, IApplicationUserService applicationUserService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.applicationUserService = applicationUserService;
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
                Brand = await this.dbContext.Brands.FirstOrDefaultAsync(b => b.Id == productForm.BrandId),
                UserId = productForm.UserId,
                User = await this.applicationUserService.GetById(productForm.UserId),
                CategoryId = productForm.CategoryId,
                Category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Id == productForm.CategoryId),
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
                                    Name = $@"{product.Id}product_{++countOfImages}",
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

        public async Task<ProductsServiceModel> GetProductsServiceModel(ProductsSorting productsSorting, string searchingName, string category, int currentPage = 1)
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

            if (!string.IsNullOrEmpty(searchingName))
            {
                searchingName = searchingName.Trim().ToLower();
                productsServiceModel.Products = productsServiceModel.Products.Where(p => p.Name.Trim().ToLower().Contains(searchingName));
            }

            return productsServiceModel;
        }

        public async Task<ProductDetailsModel> Details(int id)
        {
            Product sourceProduct = await this.dbContext.Products.AsQueryable().FirstOrDefaultAsync(p => p.Id == id);
            IEnumerable<Review> reviews = this.dbContext.Reviews.Where(r => r.ProductId == id);

            ProductDetailsModel detailsModel = new ProductDetailsModel()
            {
                Id = sourceProduct.Id,
                Name = sourceProduct.Name,
                Images = this.mapper.Map<IEnumerable<ImageViewModel>>(this.dbContext.Images.Where(i => i.ProductId == id).ToList()),
                Price = sourceProduct.Price,
                Status = sourceProduct.Status,
                Description = sourceProduct.Description,
                //Category = sourceProduct.Category.Name,
                RelatedProducts = new List<ProductViewModel>(),
                ProductReviewServiceModel = new ProductReviewServiceModel()
                {
                    TotalReviews = reviews.Count(),
                    AverageReview = reviews.Count() == 0 ? -1 : reviews.Select(r => (int)r.ReviewScale).Sum() / reviews.Count(),
                    CountOfOneStarRating = reviews.Count(r => (int)r.ReviewScale == 1),
                    CountOfTwoStarsRating = reviews.Count(r => (int)r.ReviewScale == 2),
                    CountOfThreeStarsRating = reviews.Count(r => (int)r.ReviewScale == 3),
                    CountOfFourStarsRating = reviews.Count(r => (int)r.ReviewScale == 4),
                    CountOfFiveStarsRating = reviews.Count(r => (int)r.ReviewScale == 5),
                    Reviews = this.mapper.Map<IEnumerable<ReviewViewModel>>(this.dbContext.Reviews.Where(r => r.ProductId == id)),
                },
            };

            return detailsModel;
        }

        public async Task AddReviewForProduct(AddProductReviewModel productReviewModel)
        {
            Review review = new Review()
            {
                ReviewScale = productReviewModel.ReviewScale,
                Comment = productReviewModel.Comment,
                UserId = productReviewModel.UserId,
                User = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Id == productReviewModel.UserId),
                ProductId = productReviewModel.ProductId,
                Product = await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == productReviewModel.ProductId),
                CreatedOn = DateTime.UtcNow,
            };

            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task<Product> GetByIdAsync(int id)
        {
            return await this.dbContext
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
