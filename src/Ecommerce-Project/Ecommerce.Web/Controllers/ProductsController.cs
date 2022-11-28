namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Services.Data.CategoriesServices;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Products;
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

        public async Task<IActionResult> All(string searchCategory = null, int currentPage = 1)
        {
            ProductsServiceModel productServiceModel = await this.productService.GetProductsServiceModel((searchCategory == null ? "all"  : searchCategory), currentPage);
            productServiceModel.Categories = this.categoryService.GetAll().Select(c => new ProductCategoryViewModel()
            {
                Name = c.Name,
                ProductsCount = 1,
            }); // TODO

            return View(productServiceModel);
        }
    }
}
