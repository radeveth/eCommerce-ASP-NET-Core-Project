namespace Ecommerce.Web.Controllers.ApiControllers
{
    using Ecommerce.Services.Data.ProductWishlistsServices;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductWishlistsApiController : ControllerBase
    {
        private readonly IProductWishlistService productWishlistService;

        public ProductWishlistsApiController(IProductWishlistService productWishlistService)
        {
            this.productWishlistService = productWishlistService;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> AddProductToUserWishlist(string userId, int productId)
        {
            bool result = await productWishlistService.AddProductToUserWishlist(userId, productId);

            return result;
        }
    }
}
