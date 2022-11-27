namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Services.Data.HomeServices;
    using Ecommerce.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHomeService homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            this.logger = logger;
            this.homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            HomeServiceModel serviceModel = await this.homeService.GetHomeServiceModel(4);

            return View(serviceModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}