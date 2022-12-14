namespace Ecommerce.ViewModels.Home
{
    using Ecommerce.Data.Models.Enums;
    using Ecommerce.ViewModels.Images;

    public class HomeProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public ImageViewModel Image { get; set; }

        public decimal AverageReview { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsProductIsInWishlist { get; set; }
    }
}
