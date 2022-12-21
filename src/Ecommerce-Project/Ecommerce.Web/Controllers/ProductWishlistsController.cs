namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ProductWishlistsServices;
    using Ecommerce.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductWishlistsController : BaseController
    {
        private readonly IProductWishlistService productWishlistService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

		private readonly ILogger<ProductWishlistsController> logger;

		public ProductWishlistsController(IProductWishlistService productWishlistService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<ProductWishlistsController> logger)
			: base(userManager, signInManager)
		{
			this.productWishlistService = productWishlistService;

			this.userManager = userManager;
			this.signInManager = signInManager;
			
            this.logger = logger;
		}

		[HttpGet]
        [Authorize]
        public async Task<IActionResult> AllForUser()
        {
            IEnumerable<ProductViewModel> products = await this.productWishlistService.AllForUser(this.GetUserId());

            return this.View(products);
        }
    }
}
