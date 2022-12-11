namespace Ecommerce.Services.Data.ReviewsServices
{
    using Ecommerce.ViewModels.Review;

    public interface IReviewService
    {
        public Task<ReviewVoteReturnModel> Vote(string userId, int reviewId, bool isLikeVote);
    }
}
