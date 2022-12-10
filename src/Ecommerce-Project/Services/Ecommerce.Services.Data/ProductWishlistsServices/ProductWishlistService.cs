namespace Ecommerce.Services.Data.ProductWishlistsServices
{
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class ProductWishlistService : IProductWishlistService
    {
        private readonly EcommerceDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductWishlistService(EcommerceDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<bool> AddProductToUserWishlist(string userId, int productId)
        {
            if (this.dbContext.ProductsWishlist.Any(p => p.UserId == userId && p.ProductId == productId) || !this.dbContext.Products.Any(p => p.Id == productId))
            {
                return false;
            }

            ApplicationUser applicationUser = await this.userManager.FindByIdAsync(userId);

            await this.dbContext.ProductsWishlist.AddAsync(new ProductWishlist()
            {
                UserId = userId,
                User = applicationUser,
                ProductId = productId,
                Product = this.dbContext.Products.FirstOrDefault(p => p.Id == productId),
                CreatedOn = DateTime.Now,
                IsDeleted = false,
            });

            return true;
        }
    }
}
