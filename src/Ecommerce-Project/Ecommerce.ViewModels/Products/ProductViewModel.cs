namespace Ecommerce.ViewModels.Products
{
    using Ecommerce.Data.Models.Enums;
    using Ecommerce.ViewModels.Images;

    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Status Status { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public ImageViewModel Image { get; set; }

        public decimal AverageReview { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsProductIsInCurrentUserWishlist { get; set; }
    }
}
