namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Brands;
    using Ecommerce.Services.Data.BrandsServices;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BrandsController : Controller
    {
        private readonly IBrandService brandService;
        private readonly UserManager<ApplicationUser> userManager;

        public BrandsController(IBrandService brandService, UserManager<ApplicationUser> userManager)
        {
            this.brandService = brandService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View(new BrandFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BrandFormModel brandFormModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(brandFormModel);
            }

            await this.brandService.CreateAsync(brandFormModel);

            return this.RedirectToAction(nameof(Index), "Home");
        }

        private string GetUserId()
        {
            return this.userManager.GetUserAsync(this.User).Result.Id;
        }
    }
}
