namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ProductWishlistsServices;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductWishlistsController : Controller
    {
        private readonly IProductWishlistService productWishlistService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductWishlistsController(IProductWishlistService productWishlistService, UserManager<ApplicationUser> userManager)
        {
            this.productWishlistService = productWishlistService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AllForUser()
        {
            await this.productWishlistService.AllForUser(this.GetUserId());

            return this.View();
        }

        private string GetUserId()
        {
            return this.userManager.GetUserAsync(this.User).Result.Id;
        }
    }
}
