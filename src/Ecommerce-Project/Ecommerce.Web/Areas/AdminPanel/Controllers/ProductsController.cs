namespace Ecommerce.Web.Areas.AdminPanel.Controllers
{
    using Ecommerce.InputModels.Products;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Admin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Data;

    [Area("AdminPanel")]
    [Authorize(Roles = "Administrator")]
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
        public async Task ApiDeleteAsync(int id)
        {
            await this.productService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<IActionResult> RestoreAsync(int id)
        {
            await this.productService.RestoreAsync(id);

            return this.RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public async Task ApiRestoreAsync(int id)
        {
            await this.productService.RestoreAsync(id);
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

        [HttpGet]
        public async Task<ActionResult<decimal>> SetDiscount(int id, string discountAsString)
        {
            decimal discount = decimal.Parse(discountAsString);
            await this.productService.SetDiscountToProduct(id, discount);

            return discount;
        }

        [HttpGet]
        public async Task<ActionResult<int>> SetQuantity(int id, string quantityAsString)
        {
            int quantity = int.Parse(quantityAsString);
            await this.productService.UpdateQuantityOfProduct(id, quantity);

            return quantity;
        }
    }
}
