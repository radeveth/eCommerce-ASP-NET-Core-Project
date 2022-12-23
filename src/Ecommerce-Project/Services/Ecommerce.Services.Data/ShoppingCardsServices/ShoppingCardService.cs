namespace Ecommerce.Services.Data.ShoppingCardsServices
{
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;

    public class ShoppingCardService : BaseService, IShoppingCardService
    {
        private readonly EcommerceDbContext dbContext;

        public ShoppingCardService(EcommerceDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ShoppingCard> CreateAsync(string userId, int productId)
        {
            ShoppingCard shoppingCard = new ShoppingCard()
            {
                UserId = userId,
                ProductId = productId,
            };

            await this.dbContext.ShoppingCards.AddAsync(shoppingCard);
            await this.dbContext.SaveChangesAsync();

            return shoppingCard;
        }

        public async Task AddProductToUserShoppingCardAsync(string userId, int productId)
        {
            ShoppingCard shoppingCard = this.dbContext.ShoppingCards.FirstOrDefault(sc => sc.UserId == userId);

            if (shoppingCard == null)
            {
                shoppingCard = await this.CreateAsync(userId, productId);
            }


        }
    }
}
