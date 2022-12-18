namespace Ecommerce.Web.Areas.Administartion.Controllers
{
	using Ecommerce.Web.Areas.AdminPanel.Models.Admin;
	using Ecommerce.Web.Areas.AdminPanel.Services.AdminServices;
	using Microsoft.AspNetCore.Mvc;

	[Area("AdminPanel")]
	public class AdminController : Controller
	{
		private readonly IAdminService adminService;

		public AdminController(IAdminService adminService)
		{
			this.adminService = adminService;
		}

		public async Task<IActionResult> Dashboard()
		{
			ProductsStatisticsServiceModel serviceModel = await this.adminService.GetApplicationProductsStatistics();

			return this.View(serviceModel);
		}
	}
}
