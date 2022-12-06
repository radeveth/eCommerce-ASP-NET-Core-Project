namespace Ecommerce.Web.Controllers.ApiControllers
{
    using Ecommerce.Services.Data.ApplicationUsersServices;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersApiController : ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;

        public ApplicationUsersApiController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        [HttpGet("AddProductToUserWishlist")]
        public async Task<ActionResult<bool>> AddProductToUserWishlist(string userId, int productId)
        {
            bool result = await applicationUserService.AddProductToUserWishlist(userId, productId);

            return result;
        }
    }
}
