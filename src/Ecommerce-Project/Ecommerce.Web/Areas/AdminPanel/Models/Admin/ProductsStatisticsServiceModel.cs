namespace Ecommerce.Web.Areas.AdminPanel.Models.Admin
{
	public class ProductsStatisticsServiceModel
	{
		public IEnumerable<ProductStatisticsViewModel> Products { get; set; }

		public IEnumerable<string> ProductsOrderedByPriceAscending => this.Products.OrderBy(p => p.Price).Select(p => p.Name);

		public IEnumerable<string> ProductsOrderedByPriceDescending => this.Products.OrderByDescending(p => p.Price).Select(p => p.Name);

		public int TotalProducts => this.Products.Count();

		public IEnumerable<CategoryStatisticsViewModel> Categories { get; set; }

		public IEnumerable<string> CategoriesOrderedByProductsCount => this.Categories.OrderByDescending(c => c.ProductsCount).Select(c => c.Name);
	}
}
