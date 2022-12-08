namespace Ecommerce.ViewModels.Products
{
    using Ecommerce.ViewModels.Review;

    public class ProductReviewInputModel
    {
        public decimal AverageReview { get; set; }

        public int TotalReviews { get; set; }

        public int CountOfOneStarRating { get; set; }

        public int CountOfTwoStarsRating { get; set; }

        public int CountOfThreeStarsRating { get; set; }

        public int CountOfFourStarsRating { get; set; }

        public int CountOfFiveStarsRating { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public AddProductReviewModel AddProductReviewModel { get; set; }
    }
}
