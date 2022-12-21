namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Categories;
    using Ecommerce.Services.Data.CategoriesServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public CategoriesController(ICategoryService categoryService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
            : base(userManager, signInManager)
        {
            this.categoryService = categoryService;

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return this.View(new CategoryFormModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateAsync(CategoryFormModel categoryFormModel)
        {
            categoryFormModel.UserId = this.GetUserId();

            if (!ModelState.IsValid)
            {
                return this.View(categoryFormModel);
            }

            await this.categoryService.CreateAsync(categoryFormModel);

            return this.RedirectToAction(nameof(Index), "Home");
        }
    }
}
