namespace Ecommerce.Services.Data.ReviewsServices
{
    using Ecommerce.ViewModels.Review;

    public interface IReviewService
    {
        public Task Vote(string userId, int reviewId, bool isLikeVote);

        public ReviewVoteReturnModel GetReviewVoteReturnModel(int reviewId);
    }
}
