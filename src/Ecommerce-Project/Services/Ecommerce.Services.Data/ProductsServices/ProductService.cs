namespace Ecommerce.Services.Data.ProductsServices
{
	using AutoMapper;
	using Ecommerce.Data;
	using Ecommerce.Data.Models;
	using Ecommerce.Data.Models.Enums;
	using Ecommerce.InputModels.Products;
	using Ecommerce.Services.Data.ApplicationUsersServices;
	using Ecommerce.ViewModels.Products;
	using Ecommerce.ViewModels.Products.Enums;
	using Ecommerce.ViewModels.Review;
	using Microsoft.EntityFrameworkCore;

	public class ProductService : BaseService, IProductService
	{
		private readonly IMapper mapper;
		private readonly EcommerceDbContext dbContext;
		private readonly IApplicationUserService applicationUserService;

		public ProductService(EcommerceDbContext dbContext, IMapper mapper, IApplicationUserService applicationUserService)
			: base(dbContext)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
			this.applicationUserService = applicationUserService;
		}

		// Create
		public async Task CreateAsync(ProductFormModel productForm)
		{
			Product product = new Product()
			{
				Name = productForm.Name,
				Price = productForm.Price,
				DiscountPercentage = productForm.DiscountPercentage,
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

		// Read
		public T GetByIdAsync<T>(int id)
		{
			Product product = this.GetUnDeletedProducts()
				.FirstOrDefault(p => p.Id == id);

			return this.mapper.Map<T>(product);
		}

		public IEnumerable<ProductViewModel> GetAll(string userId = null)
		{
			IEnumerable<Product> products = this.GetUnDeletedProducts();

			foreach (var product in products)
			{
				product.Category = this.dbContext.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
				product.Images = this.dbContext.Images.Where(i => i.ProductId == product.Id).ToList();
				product.Reviews = this.dbContext.Reviews.Where(i => i.ProductId == product.Id).ToList();
			}

			IEnumerable<ProductViewModel> productViewModels = this.mapper.Map<IEnumerable<ProductViewModel>>(products.AsQueryable());

			if (userId == null)
			{
				return productViewModels;
			}

			foreach (var product in productViewModels)
			{
				product.IsProductIsInCurrentUserWishlist = this.GetUnDeletedProductWishlists().Any(pw => pw.ProductId == product.Id && pw.UserId == userId);
			}

			return productViewModels;
		}

		public async Task<IEnumerable<ProductViewModel>> GetAllByCategory(string category, string userId = null)
		{
			if (category == "all")
			{
				return this.GetAll(userId);
			}

			category = category.ToLower().Replace(" ", string.Empty);
			IEnumerable<ProductViewModel> products = this.GetAll(userId).Where(p => p.CategoryName.ToLower().Replace(" ", string.Empty) == category);

			return products;
		}

		public async Task<ProductsServiceModel> GetProductsServiceModel(ProductsSorting productsSorting, string searchingName, string category, int currentPage = 1, string userId = null)
		{
			IEnumerable<ProductViewModel> products = await this.GetAllByCategory(category, userId);

			ProductsServiceModel productsServiceModel = new ProductsServiceModel()
			{
				CurrentPage = currentPage,
				SearchCategory = category == "all" ? "all" : products.Any() ? products.FirstOrDefault().CategoryName : category,
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
					ProductsSorting.BiggestDiscount => productsServiceModel.Products.OrderByDescending(p => p.DiscountPercentage),
					ProductsSorting.Latest => productsServiceModel.Products.OrderByDescending(p => p.CreatedOn),
					ProductsSorting.Ascending => productsServiceModel.Products.OrderBy(p => p.Name),
					ProductsSorting.Descending => productsServiceModel.Products.OrderByDescending(p => p.Name),
					ProductsSorting.Rating => productsServiceModel.Products.OrderByDescending(p => p.AverageReview),
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

		public async Task<ProductDetailsModel> Details(int id, string userId = null)
		{
			Product sourceProduct = this.GetUnDeletedProducts().AsQueryable().FirstOrDefault(p => p.Id == id);
			sourceProduct.Category = this.GetUnDeletedCategories().FirstOrDefault(c => c.Id == sourceProduct.CategoryId);
			sourceProduct.Brand = this.GetUnDeletedBrands().FirstOrDefault(c => c.Id == sourceProduct.BrandId);
			sourceProduct.Reviews = this.dbContext.Reviews.Where(r => r.ProductId == id).ToList();
			sourceProduct.Images = this.dbContext.Images.Where(r => r.ProductId == id).ToList();

			ProductDetailsModel detailsModel = this.mapper.Map<ProductDetailsModel>(sourceProduct);
			detailsModel.RelatedProducts = this.GetAll(userId).Where(p => p.CategoryId == sourceProduct.CategoryId && p.Id != sourceProduct.Id).OrderByDescending(p => p.AverageReview).ToList();
			detailsModel.ProductReviewServiceModel = new ProductReviewServiceModel()
			{
				TotalReviews = sourceProduct.Reviews.Count(),
				AverageReview = sourceProduct.Reviews.Count() == 0 ? -1 : sourceProduct.Reviews.Select(r => (int)r.ReviewScale).Sum() / sourceProduct.Reviews.Count(),
				CountOfOneStarRating = sourceProduct.Reviews.Count(r => (int)r.ReviewScale == 1),
				CountOfTwoStarsRating = sourceProduct.Reviews.Count(r => (int)r.ReviewScale == 2),
				CountOfThreeStarsRating = sourceProduct.Reviews.Count(r => (int)r.ReviewScale == 3),
				CountOfFourStarsRating = sourceProduct.Reviews.Count(r => (int)r.ReviewScale == 4),
				CountOfFiveStarsRating = sourceProduct.Reviews.Count(r => (int)r.ReviewScale == 5),
				Reviews = this.dbContext.Reviews.Where(r => r.ProductId == id).Select(r => new ReviewViewModel()
				{
					Id = r.Id,
					ReviewScale = r.ReviewScale,
					Comment = r.Comment,
					CreatorCommentUserName = this.dbContext.Users.FirstOrDefault(u => u.Id == r.UserId).FullName,
					Likes = this.GetUnDeletedReviewVotes().Where(rv => rv.ReviewId == r.Id && rv.UserId == r.UserId).Count(r => r.Vote == Vote.Like),
					Dislikes = this.GetUnDeletedReviewVotes().Where(rv => rv.ReviewId == r.Id && rv.UserId == r.UserId).Count(r => r.Vote == Vote.Dislike),
					CreatedOn = r.CreatedOn,
				}),
			};

			return detailsModel;
		}

		public ProductFormModel GetProductFormModel()
		{
			return new ProductFormModel()
			{
				Brands = this.mapper.Map<IEnumerable<ProductBrandFormModel>>(this.GetUnDeletedBrands()),
				Categories = this.mapper.Map<IEnumerable<ProductCategoryFormModel>>(this.GetUnDeletedCategories()),
			};
		}

        public ProductFormModel GetProductFormModelForUpdating(int id)
		{
			ProductFormModel productForm = this.GetProductFormModel();

			ProductFormModel productFormResult = this.mapper.Map<ProductFormModel>(this.GetByIdUndeletedProduct(id));
			productFormResult.Categories = productForm.Categories;
			productFormResult.Brands = productForm.Brands;

            return productFormResult;
		}

        // Update
        public async Task UpdateAsync(int id, ProductFormModel productForm)
        {
            Product product = this.GetByIdUndeletedProduct(id);

            product.Name = productForm.Name;
            product.Quantity = productForm.Quantity;
            product.DiscountPercentage = productForm.DiscountPercentage;
            product.BrandId = productForm.BrandId;
            product.CategoryId = productForm.CategoryId;
            product.Description = productForm.Description;
            product.Price = productForm.Price;
            product.Status = productForm.Status;
            product.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task RestoreAsync(int id)
        {
            Product product = this.GetById(id);

            product.IsDeleted = false;
            product.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task SetDiscountToProduct(int id, decimal discount)
        {
            Product product = this.GetUnDeletedProducts().FirstOrDefault(p => p.Id == id);

            product.DiscountPercentage = discount;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateDiscountStatusOfProduct(int id, bool discountStatus)
        {
            Product product = this.GetUnDeletedProducts().FirstOrDefault(p => p.Id == id);

            product.IsHaveDiscount = discountStatus;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateQuantityOfProduct(int id, int quantity)
        {
            Product product = this.GetByIdUndeletedProduct(id);
            product.Quantity = quantity;

            if (product.Quantity == 0)
            {
                product.Status = Status.Unavailable;
            }
            else if (product.Quantity != 0)
            {
                product.Status = Status.Available;
            }

            await this.dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            Product product = this.GetByIdUndeletedProduct(id);

            product.DeletedOn = DateTime.Now;
            product.IsDeleted = true;

            await this.dbContext.SaveChangesAsync();
        }

        // Usefull Methods
        private Product GetByIdUndeletedProduct(int id)
        {
            return this
                .GetUnDeletedProducts()
				.AsQueryable()
                .FirstOrDefault(p => p.Id == id);
        }

        private Product GetById(int id)
        {
            return this.dbContext
				.Products
				.AsQueryable()
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
