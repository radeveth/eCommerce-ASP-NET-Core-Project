namespace Ecommerce.Web.Areas.AdminPanel.Controllers
{
    using Ecommerce.InputModels.Products;
    using Ecommerce.Services.Data.ProductsServices;
    using Microsoft.AspNetCore.Mvc;

    [Area("AdminPanel")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.productService.DeleteAsync(id);

            return this.RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> RestoreAsync(int id)
        {
            await this.productService.RestoreAsync(id);

            return this.RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ProductFormModel productForm = this.productService.GetProductFormModelForUpdating(id);

            return this.View(productForm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int id, ProductFormModel productForm)
        {
            await this.productService.UpdateAsync(id, productForm);

            return this.RedirectToAction("Dashboard", "Admin");
        }
    }
}
