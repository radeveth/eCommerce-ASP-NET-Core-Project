namespace Ecommerce.Services.Data.ProductWishlistsServices
{
    using Ecommerce.ViewModels.ProductWishlists;
    using System.Collections.Generic;

    public interface IProductWishlistService
    {
        public Task<bool> AddProductToUserWishlist(string userId, int productId);

        public Task<IEnumerable<ProductWishlistViewModel>> AllForUser(string userId);
    }
}
