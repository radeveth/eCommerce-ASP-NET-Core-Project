namespace Ecommerce.Services.Data.ShoppingCardsServices
{
    using Ecommerce.Data.Models;

    public interface IShoppingCardService
    {
        public Task<ShoppingCard> CreateAsync(string userId, int productId);

        public Task AddProductToUserShoppingCardAsync(string userId, int productId);
    }
}