namespace Ecommerce.Services.Data.ShoppingCardsServices
{
    using Ecommerce.Data.Models;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.ShoppingCard;

    public interface IShoppingCardService
    {
        // Create
        public Task<ShoppingCard> CreateAsync(string userId, int productId);

        public Task AddProductToUserShoppingCardAsync(string userId, int productId);

        // Read
        public ShoppingCardServiceModel GetUserShoppingCard(string userId, int currentPage = 1);
    }
}