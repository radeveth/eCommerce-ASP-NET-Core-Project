namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

		public BaseController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public string GetUserId()
        {
            return this.signInManager.IsSignedIn(this.User) == true ? this.userManager.GetUserId(this.User) : null;
        }
    }
}
