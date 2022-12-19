namespace Ecommerce.Web.Areas.AdminPanel.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Categories;
    using Ecommerce.InputModels.Products;
    using Ecommerce.Services.Data.CategoriesServices;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.categoryService.DeleteAsync(id);

            return this.RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> RestoreAsync(int id)
        {
            await this.categoryService.RestoreAsync(id);

            return this.RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return this.View(new CategoryFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int id, CategoryFormModel categoryForm)
        {
            await this.categoryService.UpdateAsync(id, categoryForm);

            return this.RedirectToAction("Dashboard", "Admin");
        }
    }
}
