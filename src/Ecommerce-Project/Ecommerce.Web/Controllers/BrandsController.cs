namespace Ecommerce.Web.Controllers
{
    using Ecommerce.InputModels.Brands;
    using Ecommerce.Services.Data.BrandsServices;
    using Microsoft.AspNetCore.Mvc;

    public class BrandsController : Controller
    {
        private readonly IBrandService brandService;

        public BrandsController(IBrandService brandService)
        {
            this.brandService = brandService;
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
    }
}
