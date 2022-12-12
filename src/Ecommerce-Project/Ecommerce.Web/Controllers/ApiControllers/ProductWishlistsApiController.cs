namespace Ecommerce.Web.Controllers.ApiControllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ProductWishlistsServices;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductWishlistsApiController : ControllerBase
    {
        private readonly IProductWishlistService productWishlistService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductWishlistsApiController(IProductWishlistService productWishlistService, UserManager<ApplicationUser> userManager)
        {
            this.productWishlistService = productWishlistService;
            this.userManager = userManager;
        }

        [HttpGet("Add")]
        public async Task AddProductToUserWishlist(int productId)
        {
            await productWishlistService.AddProductToUserWishlist(this.GetUserId(), productId);
        }

        [HttpGet("Remove")]
        public async Task RemoveProductFromUserWishlist(int productId)
        {
            await productWishlistService.RemoveProductFromUserWishlist(this.GetUserId(), productId);
        }

        private string GetUserId()
        {
            return this.userManager.GetUserAsync(this.User).Result.Id;
        }
    }
}
