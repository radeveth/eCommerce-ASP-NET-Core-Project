namespace Ecommerce.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProductWishlistsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> AllForUser(string userId)
        {
            return this.View();
        }
    }
}
