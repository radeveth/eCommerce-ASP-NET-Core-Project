namespace eCommerceAPI.ViewModels.Products
{
    using eCommerceAPI.Data.Models.Enums;
    using eCommerceAPI.ViewModels.Review;

    public class ProductViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Status Status { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public IEnumerable<ProductImageViewModel> Images { get; set; }

        public decimal AverageReview { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}
