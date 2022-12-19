namespace Ecommerce.ViewModels.Admin
{
	public class CategoryStatisticsViewModel
	{
		public int Id { get; set; }


		public string Name { get; set; }

		public int ProductsCount { get; set; }

		public bool IsDeleted { get; set; }
	}
}
