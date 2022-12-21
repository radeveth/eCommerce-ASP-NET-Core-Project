namespace Ecommerce.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : Controller
    {
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
    }
}
