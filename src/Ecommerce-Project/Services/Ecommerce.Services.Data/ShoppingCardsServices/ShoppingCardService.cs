namespace Ecommerce.Services.Data.ShoppingCardsServices
{
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.ShoppingCard;

    public class ShoppingCardService : BaseService, IShoppingCardService
    {
        private readonly EcommerceDbContext dbContext;
        private readonly IProductService productService;

        public ShoppingCardService(EcommerceDbContext dbContext, IProductService productService)
            : base(dbContext)
        {
            this.dbContext = dbContext;
            this.productService = productService;
        }

        // Create
        public async Task<ShoppingCard> CreateAsync(string userId, int productId)
        {
            ShoppingCard shoppingCard = new ShoppingCard()
            {
                UserId = userId,
            };

            await this.dbContext.ShoppingCards.AddAsync(shoppingCard);
            await this.dbContext.SaveChangesAsync();

            return shoppingCard;
        }

        public async Task AddProductToUserShoppingCardAsync(string userId, int productId)
        {
            ShoppingCard shoppingCard = this.GetByUserId(userId);

            if (shoppingCard == null)
            {
                shoppingCard = await this.CreateAsync(userId, productId);
            }

            ShoppingCardProduct shoppingCardProduct = new ShoppingCardProduct()
            {
                ShoppingCardId = shoppingCard.Id,
                ProductId = productId,
            };

            await this.dbContext.ShoppingCardProducts.AddAsync(shoppingCardProduct);
            await this.dbContext.SaveChangesAsync();
        }

        // Read
        public ShoppingCardServiceModel GetUserShoppingCard(string userId, int currentPage = 1)
        {
            ShoppingCard shoppingCard = this.GetByUserId(userId);

            IEnumerable<int> productIds = this.dbContext.ShoppingCardProducts
                .Where(scp => scp.ShoppingCardId == shoppingCard.Id)
                .Select(scp => scp.ProductId);

            List<ProductViewModel> products = new List<ProductViewModel>();

            foreach (var product in this.productService.GetAll(userId))
            {
                if (productIds.Contains(product.Id))
                {
                    products.Add(product);
                }
            }

            return new ShoppingCardServiceModel()
            {
                Products = products
                    .Skip(currentPage - 1 * ShoppingCardServiceModel.ProductsPerPage)
                    .Take(ShoppingCardServiceModel.ProductsPerPage),
                TotalProducts = products.Count,
                CurrentPage = currentPage,
            };
        }

        private ShoppingCard GetById(int id)
        {
            return this.GetUnDeletedShoppingCards()
                       .FirstOrDefault(sc => sc.Id == id);
        }

        private ShoppingCard GetByUserId(string id)
        {
            return this.GetUnDeletedShoppingCards()
                       .FirstOrDefault(sc => sc.UserId == id);
        }
    }
}
