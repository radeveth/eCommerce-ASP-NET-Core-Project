namespace Ecommerce.Services.Data.ProductWishlistsServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.ProductWishlists;
    using Microsoft.AspNetCore.Identity;

    public class ProductWishlistService : IProductWishlistService
    {
        private readonly EcommerceDbContext dbContext;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductWishlistService(EcommerceDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper, IProductService productService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.productService = productService;
        }

        public async Task AddProductToUserWishlist(string userId, int productId)
        {
            if (this.GetUnDeletedProductWishlists().Any(p => p.UserId == userId && p.ProductId == productId))
            {
                return;
            }

            ProductWishlist productWishlist = new ProductWishlist()
            {
                UserId = userId,
                ProductId = productId,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
            };

            await this.dbContext.ProductsWishlist.AddAsync(productWishlist);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveProductFromUserWishlist(string userId, int productId)
        {
            ProductWishlist productWishlist = this.GetUnDeletedProductWishlists().FirstOrDefault(p => p.UserId == userId && p.ProductId == productId);

            if (productWishlist == null)
            {
                return;
            }

            this.dbContext.ProductsWishlist.Remove(productWishlist);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> AllForUser(string userId)
        {
            //return this.mapper.Map<IEnumerable<ProductWishlistViewModel>>(this.GetUnDeletedProductWishlists().Where(p => p.UserId == userId));
            return this.productService.GetAll();
        }

        public bool IsProductIsInUserWishlist(string userId, int productId)
        {
            return this.GetUnDeletedProductWishlists().Any(pw => pw.UserId == userId && pw.ProductId == productId);
        }

        private IEnumerable<ProductWishlist> GetUnDeletedProductWishlists()
        {
            return this.dbContext.ProductsWishlist.Where(pw => pw.IsDeleted == false);
        }
    }
}
