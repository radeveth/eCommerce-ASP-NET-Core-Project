namespace Ecommerce.ViewModels.Admin
{
    public class ProductsStatisticsServiceModel
    {
        public IEnumerable<ProductStatisticsViewModel> Products { get; set; }

        public IEnumerable<ProductPriceViewModel> ProductsOrderedByPriceAscending => this.Products
            .Where(p => p.IsDeleted == false)
            .OrderBy(p => p.Price).Select(p => new ProductPriceViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            })
            .Take(5);

        public IEnumerable<ProductPriceViewModel> ProductsOrderedByPriceDescending => this.Products
            .Where(p => p.IsDeleted == false)
            .OrderByDescending(p => p.Price).Select(p => new ProductPriceViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            })
            .Take(5);

        public int ProductsCount => this.Products.Count();

        public IEnumerable<CategoryStatisticsViewModel> Categories { get; set; }

        public IEnumerable<string> CategoriesOrderedByProductsCount => this.Categories.OrderByDescending(c => c.ProductsCount).Select(c => c.Name);

        public int CategoriesCount => this.Categories.Count();
    }

    public class ProductPriceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
