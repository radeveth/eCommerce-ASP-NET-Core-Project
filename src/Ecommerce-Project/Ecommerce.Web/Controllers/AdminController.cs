namespace Ecommerce.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
