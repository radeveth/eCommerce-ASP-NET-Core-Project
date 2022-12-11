namespace Ecommerce.Web.Controllers.ApiControllers
{
    using Ecommerce.ViewModels.Review;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsApiController : ControllerBase
    {
        [HttpGet("Vote")]
        public async Task<ActionResult<ReviewVoteReturnModel>> Vote(int reviewId, bool isLikeVote)
        {


            return new ReviewVoteReturnModel()
            {

            };
        }
    }
}
