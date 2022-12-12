namespace Ecommerce.Services.Data.ReviewsServices
{
    using System.Threading.Tasks;
    using Ecommerce.Data;
    using Ecommerce.Data.Models;
    using Ecommerce.ViewModels.Review;

    public class ReviewService : IReviewService
    {
        private readonly EcommerceDbContext dbContext;

        public ReviewService(EcommerceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Vote(string userId, int reviewId, bool isLikeVote)
        {
            ReviewVote reviewVote = this.dbContext.ReviewVotes.FirstOrDefault(rv => rv.UserId == userId && rv.ReviewId == reviewId);

            if (reviewVote != null)
            {
                reviewVote.Vote = isLikeVote ? Ecommerce.Data.Models.Enums.Vote.Like : Ecommerce.Data.Models.Enums.Vote.Dislike;
                reviewVote.ModifiedOn = DateTime.UtcNow;

                return;
            }

            ReviewVote vote = new ReviewVote()
            {
                UserId = userId,
                ReviewId = reviewId,
                Vote = isLikeVote ? Ecommerce.Data.Models.Enums.Vote.Like : Ecommerce.Data.Models.Enums.Vote.Dislike,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            await this.dbContext.ReviewVotes.AddAsync(vote);
            await this.dbContext.SaveChangesAsync();
        }

        public ReviewVoteReturnModel GetReviewVoteReturnModel(int reviewId)
        {
            List<ReviewVote> reviewVotes = this.dbContext
                    .ReviewVotes
                    .Where(rv => rv.ReviewId == reviewId)
                    .ToList();

            return new ReviewVoteReturnModel()
            {
                Likes = reviewVotes.Count(rv => rv.Vote == Ecommerce.Data.Models.Enums.Vote.Like),
                Dislikes = reviewVotes.Count(rv => rv.Vote == Ecommerce.Data.Models.Enums.Vote.Dislike),
            };
        }
    }
}
