namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.HomeServices;
    using Ecommerce.ViewModels.Home;
    using Ecommerce.Web.Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        
        private readonly ILogger<HomeController> logger;

        public HomeController(IHomeService homeService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger)
            : base(userManager, signInManager)
        {
            this.logger = logger;
            this.homeService = homeService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeServiceModel serviceModel = await this.homeService.GetHomeServiceModel(6, this.GetUserId());

            return View(serviceModel);
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            return this.View();
        }

        [HttpGet]
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