namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Services.Data.CategoriesServices;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.Products.Enums;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> All([FromQuery] ProductsServiceModel productsServiceModel)
        {
            ProductsServiceModel productServiceModel = await this.productService.GetProductsServiceModel(productsServiceModel.ProductsSorting, (productsServiceModel.SearchCategory == null ? "all" : productsServiceModel.SearchCategory), productsServiceModel.CurrentPage);

            IEnumerable<ProductCategoryViewModel> categories = this.categoryService.GetAll().Select(c => new ProductCategoryViewModel()
            {
                Name = c.Name,
                ProductsCount = c.ProductsCount,
            });

            productServiceModel.Categories = categories.OrderByDescending(c => c.ProductsCount);

            return View(productServiceModel);
        }

        public async Task<IActionResult> Details(int id)
        {


            return this.View();
        }
    }
}
