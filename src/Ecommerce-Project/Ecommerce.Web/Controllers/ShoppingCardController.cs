namespace Ecommerce.Web.Controllers
{
    using Ecommerce.Services.Data.ShoppingCardsServices;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCardController : Controller
    {
        private readonly IShoppingCardService shoppingCardService;

        public ShoppingCardController(IShoppingCardService shoppingCardService)
        {
            this.shoppingCardService = shoppingCardService;
        }

        [HttpGet]
        public async Task AddProductToUserShoppingCard(string userId, int productId)
        {
            await this.shoppingCardService.AddProductToUserShoppingCardAsync(userId, productId);
        }
    }
}
