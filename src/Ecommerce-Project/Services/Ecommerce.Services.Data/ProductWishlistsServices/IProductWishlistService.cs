namespace Ecommerce.Services.Data.ProductWishlistsServices
{
    using System.Collections.Generic;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.ProductWishlists;

    public interface IProductWishlistService
    {
        public Task AddProductToUserWishlist(string userId, int productId);

        public Task RemoveProductFromUserWishlist(string userId, int productId);

        public bool IsProductIsInUserWishlist(string userId, int productId);

        public Task<IEnumerable<ProductViewModel>> AllForUser(string userId);
    }
}
