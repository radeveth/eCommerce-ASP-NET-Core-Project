namespace Ecommerce.Web.Controllers
{
    using Ecommerce.InputModels.Categories;
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
        public IActionResult Create()
        {
            return this.View(new CategoryFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryFormModel categoryFormModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(categoryFormModel);
            }

            await this.categoryService.CreateAsync(categoryFormModel);

            return this.RedirectToAction(nameof(Index), "Home");
        }
    }
}
