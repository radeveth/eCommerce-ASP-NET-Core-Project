namespace Ecommerce.Web.Areas.Administartion.Controllers
{
	using Ecommerce.Services.Data.AdminServices;
	using Ecommerce.ViewModels.Admin;
	using Microsoft.AspNetCore.Mvc;

	[Area("AdminPanel")]
	public class AdminController : Controller
	{
		private readonly IAdminService adminService;

		public AdminController(IAdminService adminService)
		{
			this.adminService = adminService;
		}

		[HttpGet]
		public async Task<IActionResult> ProductsDashboard()
		{
			ProductsStatisticsServiceModel serviceModel = await this.adminService.GetApplicationProductsStatistics();

			return this.View(serviceModel);
		}

		[HttpGet]
		public async Task<IActionResult> UsersDashboard()
		{
			return this.View();
		}
	}
}
