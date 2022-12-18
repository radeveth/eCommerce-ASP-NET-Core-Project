namespace Ecommerce.Web.Areas.AdminPanel.Services
{
    using Ecommerce.Data.Models;
    using Ecommerce.Data;

    public class BaseService
    {
        private readonly EcommerceDbContext dbContext;

        public BaseService(EcommerceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Product> GetUnDeletedProducts()
        {
            return this.dbContext.Products.Where(p => p.IsDeleted == false);
        }

        public IEnumerable<Category> GetUnDeletedCategories()
        {
            return this.dbContext.Categories.Where(p => p.IsDeleted == false);
        }

        public IEnumerable<Brand> GetUnDeletedBrands()
        {
            return this.dbContext.Brands.Where(p => p.IsDeleted == false);
        }

        public IEnumerable<ProductWishlist> GetUnDeletedProductWishlists()
        {
            return this.dbContext.ProductsWishlist.Where(p => p.IsDeleted == false);
        }

        public IEnumerable<Order> GetUnDeletedOrders()
        {
            return this.dbContext.Orders.Where(p => p.IsDeleted == false);
        }

        public IEnumerable<OrderAddress> GetUnDeletedOrderAddress()
        {
            return this.dbContext.OrderAddresses.Where(p => p.IsDeleted == false);
        }

        public IEnumerable<ReviewVote> GetUnDeletedReviewVotes()
        {
            return this.dbContext.ReviewVotes.Where(p => p.IsDeleted == false);
        }

        public IEnumerable<Image> GetUnDeletedImages()
        {
            return this.dbContext.Images.Where(p => p.IsDeleted == false);
        }
    }
}
