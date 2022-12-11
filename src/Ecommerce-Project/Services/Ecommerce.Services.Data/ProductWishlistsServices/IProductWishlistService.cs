namespace Ecommerce.Services.Data.ProductWishlistsServices
{
    using System.Collections.Generic;
    using Ecommerce.ViewModels.ProductWishlists;

    public interface IProductWishlistService
    {
        public Task<bool> AddProductToUserWishlist(string userId, int productId);

        public Task<IEnumerable<ProductWishlistViewModel>> AllForUser(string userId);
    }
}
