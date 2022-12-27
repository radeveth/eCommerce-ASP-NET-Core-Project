namespace Ecommerce.Services.Data.ShoppingCardsServices
{
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ProductsServices;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.ShoppingCart;
    using Ecommerce.ViewModels.ShoppingCart;

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
        public async Task<ShoppingCart> CreateAsync(string userId, int productId)
        {
            ShoppingCart shoppingCard = new ShoppingCart()
            {
                UserId = userId,
            };

            await this.dbContext.ShoppingCarts.AddAsync(shoppingCard);
            await this.dbContext.SaveChangesAsync();

            return shoppingCard;
        }

        public async Task AddProductToUserShoppingCardAsync(string userId, int productId)
        {
            ShoppingCart shoppingCard = this.GetByUserId(userId);

            if (shoppingCard == null)
            {
                shoppingCard = await this.CreateAsync(userId, productId);
            }

            ShoppingCartProduct shoppingCardProduct = this.GetShoppingCradProduct(shoppingCard.Id, productId);

            if (shoppingCardProduct != null)
            {
                shoppingCardProduct.Count++;
                shoppingCardProduct.ModifiedOn = DateTime.UtcNow;
            }
            else
            {
                shoppingCardProduct = new ShoppingCartProduct()
                {
                    ShoppingCardId = shoppingCard.Id,
                    ProductId = productId,
                    Count = 1,
                };

                await this.dbContext.ShoppingCartProducts.AddAsync(shoppingCardProduct);
            }

            await this.dbContext.SaveChangesAsync();
        }

        // Read
        public ShoppingCartServiceModel GetUserShoppingCard(string userId, int currentPage = 1)
        {
            ShoppingCart shoppingCard = this.GetByUserId(userId);

            IEnumerable<int> productIds = this.dbContext.ShoppingCartProducts
                .Where(scp => scp.ShoppingCardId == shoppingCard.Id)
                .Select(scp => scp.ProductId);

            List<ProductViewModel> products = new List<ProductViewModel>();

            foreach (var product in this.productService.GetAll(userId))
            {
                if (productIds.Contains(product.Id))
                {
                    product.AddedTimesToShoppingCart = this.GetShoppingCradProduct(shoppingCard.Id, product.Id).Count;
                    products.Add(product);
                }
            }

            return new ShoppingCartServiceModel()
            {
                Products = products
                    .Skip(currentPage - 1 * ShoppingCartServiceModel.ProductsPerPage)
                    .Take(ShoppingCartServiceModel.ProductsPerPage),
                TotalProducts = products.Count,
                CurrentPage = currentPage,
            };
        }

        // Usefull methods
        private ShoppingCart GetById(int id)
        {
            return this.GetUnDeletedShoppingCards()
                       .FirstOrDefault(sc => sc.Id == id);
        }

        private ShoppingCart GetByUserId(string id)
        {
            return this.GetUnDeletedShoppingCards()
                       .FirstOrDefault(sc => sc.UserId == id);
        }

        private ShoppingCartProduct GetShoppingCradProduct(int shoppingCardId, int productId)
        {
            return this.dbContext.ShoppingCartProducts
                .FirstOrDefault(scp => scp.ShoppingCardId == shoppingCardId && scp.ProductId == productId);
        }
    }
}
