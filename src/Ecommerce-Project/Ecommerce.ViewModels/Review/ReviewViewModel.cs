namespace Ecommerce.ViewModels.Review
{
    using Ecommerce.Data.Models.Enums;

    public class ReviewViewModel
    {
        public ReviewScale ReviewScale { get; set; }

        public string Comment { get; set; }

        public string CreateorCommentUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
