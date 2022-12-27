namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ShoppingCardsServices;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCardService shoppingCardService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ShoppingCartController(IShoppingCardService shoppingCardService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
            : base(userManager, signInManager)
        {
            this.shoppingCardService = shoppingCardService;

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [Authorize]
        public async Task AddProductToUserShoppingCart(int productId)
        {
            await this.shoppingCardService.AddProductToUserShoppingCardAsync(this.GetUserId(), productId);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserShoppingCart(int currentPage = 1)
        {
            ShoppingCartServiceModel serviceModel = this.shoppingCardService.GetUserShoppingCard(this.GetUserId(), currentPage);

            return this.View(serviceModel);
        }
    }
}
