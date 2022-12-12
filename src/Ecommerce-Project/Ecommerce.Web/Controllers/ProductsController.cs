namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Products;
    using Ecommerce.Services.Data.CategoriesServices;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.Services.Data.ProductWishlistsServices;
    using Ecommerce.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IProductWishlistService productWishlistService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(IProductService productService, ICategoryService categoryService, UserManager<ApplicationUser> user, IProductWishlistService productWishlistService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userManager = user;
            this.productWishlistService = productWishlistService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductFormModel productForm = this.productService.GetProductFormModel();

            return this.View(productForm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromForm] ProductFormModel productForm)
        {
            productForm.UserId = this.GetUserId();

            if (!ModelState.IsValid)
            {
                return this.View(productForm);
            }

            await this.productService.CreateAsync(productForm);

            return this.RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> All([FromQuery] ProductsServiceModel productsServiceModel)
        {
            ProductsServiceModel productServiceModel = await this.productService.GetProductsServiceModel(productsServiceModel.ProductsSorting, productsServiceModel.SearchNameCriteria, (productsServiceModel.SearchCategory == null ? "all" : productsServiceModel.SearchCategory), productsServiceModel.CurrentPage);

            IEnumerable<ViewModels.Products.ProductCategoryViewModel> categories = this.categoryService.GetAll().Select(c => new ViewModels.Products.ProductCategoryViewModel()
            {
                Name = c.Name,
                ProductsCount = c.ProductsCount,
            });

            productServiceModel.Categories = categories.OrderByDescending(c => c.ProductsCount);

            return View(productServiceModel);
        }

        public async Task<IActionResult> Details(int id, bool isHaveProductReviewError = false)
        {
            ProductDetailsModel productDetails = await this.productService.Details(id);

            if (isHaveProductReviewError == true)
            {
                ViewData["IsHaveProductReviewError"] = true;
            }
            else
            {
                ViewData["IsHaveProductReviewError"] = false;
            }

            ViewData["IsProductIsInUserWishlist"] = this.productWishlistService.IsProductIsInUserWishlist(this.GetUserId(), id);
            return this.View(productDetails);
        }

        [HttpPost]
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

        private string GetUserId()
        {
            return this.userManager.GetUserAsync(this.User).Result.Id;
        }
    }
}
