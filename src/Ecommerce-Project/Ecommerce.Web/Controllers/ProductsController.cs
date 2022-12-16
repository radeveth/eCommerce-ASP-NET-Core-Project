namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Products;
    using Ecommerce.Services.Data.CategoriesServices;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.Services.Data.ProductWishlistsServices;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IProductWishlistService productWishlistService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
		
        private readonly ILogger<ProductsController> logger;

		public ProductsController(IProductService productService, ICategoryService categoryService, IProductWishlistService productWishlistService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<ProductsController> logger)
			: base(userManager, signInManager)
		{
			this.productService = productService;
			this.categoryService = categoryService;
			this.productWishlistService = productWishlistService;
			
            this.signInManager = signInManager;
			this.userManager = userManager;
			
            this.logger = logger;
		}

		[HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ProductFormModel productForm = this.productService.GetProductFormModel();

            return this.View(productForm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromForm] ProductFormModel productForm)
        {
            productForm.UserId = ClaimsPrincipalExtensions.GetUserId(this.User);

            if (!ModelState.IsValid)
            {
                return this.View(productForm);
            }

            await this.productService.CreateAsync(productForm);

            return this.RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] ProductsServiceModel productsServiceModel)
        {
            ProductsServiceModel productServiceModel = await this.productService.GetProductsServiceModel(productsServiceModel.ProductsSorting, productsServiceModel.SearchNameCriteria, (productsServiceModel.SearchCategory == null ? "all" : productsServiceModel.SearchCategory), productsServiceModel.CurrentPage, ClaimsPrincipalExtensions.GetUserId(this.User));

            return View(productServiceModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, bool isHaveProductReviewError = false)
        {
            ProductDetailsModel productDetails = await this.productService.Details(id, ClaimsPrincipalExtensions.GetUserId(this.User));

            if (isHaveProductReviewError == true)
            {
                ViewData["IsHaveProductReviewError"] = true;
            }
            else
            {
                ViewData["IsHaveProductReviewError"] = false;
            }

            if (this.signInManager.IsSignedIn(this.User))
            {
                ViewData["IsProductIsInUserWishlist"] = this.productWishlistService.IsProductIsInUserWishlist(ClaimsPrincipalExtensions.GetUserId(this.User), id);
            }
            else
            {
                ViewData["IsProductIsInUserWishlist"] = false;
            }

            return this.View(productDetails);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReviewForProduct(ProductDetailsModel productDetails)
        {
            if (productDetails.ProductReviewServiceModel.AddProductReviewModel.ReviewScale == 0)
            {
                return this.RedirectToAction(nameof(Details), new
                {
                    id = productDetails.ProductReviewServiceModel.AddProductReviewModel.ProductId,
                    isHaveProductReviewError = true,
                });
            }

            await this.productService.AddReviewForProduct(productDetails.ProductReviewServiceModel.AddProductReviewModel);

            return this.RedirectToAction(nameof(Details), new { id = productDetails.ProductReviewServiceModel.AddProductReviewModel.ProductId, });
        }
    }
}
