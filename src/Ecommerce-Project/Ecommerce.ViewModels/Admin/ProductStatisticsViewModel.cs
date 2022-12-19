namespace Ecommerce.ViewModels.Admin
{
	using Ecommerce.Data.Models.Enums;

	public class ProductStatisticsViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public int Quantity { get; set; }

		public Status Status { get; set; }

		public decimal DiscountPercentage { get; set; }

		public bool IsHaveDiscount { get; set; }

		public bool IsDeleted { get; set; }
	}
}
