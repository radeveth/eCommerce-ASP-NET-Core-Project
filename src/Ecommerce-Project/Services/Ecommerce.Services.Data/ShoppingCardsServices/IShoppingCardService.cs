namespace Ecommerce.Services.Data.ShoppingCardsServices
{
    using Ecommerce.Data.Models;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.ShoppingCart;
    using Ecommerce.ViewModels.ShoppingCart;

    public interface IShoppingCardService
    {
        // Create
        public Task<ShoppingCart> CreateAsync(string userId, int productId);

        public Task AddProductToUserShoppingCardAsync(string userId, int productId);

        // Read
        public ShoppingCartServiceModel GetUserShoppingCard(string userId, int currentPage = 1);
    }
}