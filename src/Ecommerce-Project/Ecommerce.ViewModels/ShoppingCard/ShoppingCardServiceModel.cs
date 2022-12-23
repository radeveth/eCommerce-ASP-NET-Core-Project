namespace Ecommerce.ViewModels.ShoppingCard
{
    using Ecommerce.ViewModels.Products;

    public class ShoppingCardServiceModel
    {
        public const int ProductsPerPage = 12;

        public int TotalProducts { get; set; }

        public int CurrentPage { get; set; } = 1;

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
