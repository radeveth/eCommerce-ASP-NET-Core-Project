namespace Ecommerce.ViewModels.Products
{
    public class ProductsServiceModel
    {
        public const int ProductsPerPage = 12;

        public int CurrentPage { get; set; }

        public string SearchingCategory { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
    }
}
