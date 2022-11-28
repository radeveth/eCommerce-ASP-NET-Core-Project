namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> All(string searchCategory = null)
        {
            IEnumerable<ProductViewModel> productViews = await this.productService.GetByAllProductsForCategory(searchCategory);

            return View();
        }
    }
}
