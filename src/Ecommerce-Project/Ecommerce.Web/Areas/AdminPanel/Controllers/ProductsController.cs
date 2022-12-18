namespace Ecommerce.Web.Areas.AdminPanel.Controllers
{
    using Ecommerce.Web.Areas.AdminPanel.Services.ProductsServices;
    using Microsoft.AspNetCore.Mvc;

    [Area("AdminPanel")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.productService.DeleteAsync(id);

            return this.RedirectToAction();
        }

        [HttpPost]
        public IActionResult Restore(int id)
        {
            return this.RedirectToAction();
        }
    }
}
