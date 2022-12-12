namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ProductWishlistsServices;
    using Ecommerce.ViewModels.Products;
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
            IEnumerable<ProductViewModel> products = await this.productWishlistService.AllForUser(this.GetUserId());

            return this.View(products);
        }

        private string GetUserId()
        {
            return this.userManager.GetUserAsync(this.User).Result.Id;
        }
    }
}
