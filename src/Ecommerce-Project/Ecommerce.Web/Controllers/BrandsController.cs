namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.InputModels.Brands;
    using Ecommerce.Services.Data.BrandsServices;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BrandsController : BaseController
    {
        private readonly IBrandService brandService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
		
        private readonly ILogger<BrandsController> logger;

		public BrandsController(IBrandService brandService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<BrandsController> logger)
			: base(userManager, signInManager)
		{
			this.brandService = brandService;

			this.userManager = userManager;
			this.signInManager = signInManager;
			
            this.logger = logger;
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
