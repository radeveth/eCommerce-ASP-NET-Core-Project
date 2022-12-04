namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Products;
    using Ecommerce.Services.Data.CategoriesServices;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly UserManager<ApplicationUser> user;

        public ProductsController(IProductService productService, ICategoryService categoryService, UserManager<ApplicationUser> user)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.user = user;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductFormModel productForm = this.productService.GetProductFormModel();

            return this.View(productForm);
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> CreateAsync([FromForm] ProductFormModel productForm)
        {
            // productForm.UserId = this.GetUserId();
            productForm.UserId = "46fca1c1-4608-42dd-8fb4-88d284690463";

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

        public async Task<IActionResult> Details(int id)
        {
            ProductDetailsModel productDetails = await this.productService.Details(id);

            return this.View(productDetails);
        }

        private string GetUserId()
        {
            return this.user.GetUserAsync(this.User).Result.Id;
        }
    }
}
