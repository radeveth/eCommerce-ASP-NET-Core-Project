namespace Ecommerce.Web.Areas.Administartion.Controllers
{
	using Ecommerce.Services.Data.AdminServices;
	using Ecommerce.ViewModels.Admin;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Area("AdminPanel")]
	[Authorize(Roles = "Administrator")]
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
			UsersStatistsicsServiceModel serviceModel = await this.adminService.GetApplicationUsersStatistics();

			return this.View(serviceModel);
		}

		[HttpGet]
		public async Task DeleteUser(string id)
		{
			await this.adminService.DeleteUser(id);
		}

		[HttpGet]
		public async Task RestoreUser(string id)
		{
			await this.adminService.RestoreUser(id);
		}
	}
}
