namespace Ecommerce.Web.Controllers.ApiControllers
{
    using Ecommerce.Data.Models;
    using Ecommerce.Services.Data.ReviewsServices;
    using Ecommerce.ViewModels.Review;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsApiController : ControllerBase
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsApiController(IReviewService reviewService, UserManager<ApplicationUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpGet("Vote")]
        public async Task<ActionResult<ReviewVoteReturnModel>> Vote(int reviewId, bool isLikeVote)
        {
            await this.reviewService.Vote(this.GetUserId(), reviewId, isLikeVote);

            return this.reviewService.GetReviewVoteReturnModel(reviewId);
        }

        private string GetUserId()
        {
            return this.userManager.GetUserAsync(this.User).Result.Id;
        }
    }
}
