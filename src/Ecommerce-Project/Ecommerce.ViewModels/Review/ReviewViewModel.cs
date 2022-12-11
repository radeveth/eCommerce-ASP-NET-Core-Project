namespace Ecommerce.ViewModels.Review
{
    using Ecommerce.Data.Models.Enums;

    public class ReviewViewModel
    {
        public int Id { get; set; }

        public ReviewScale ReviewScale { get; set; }

        public string Comment { get; set; }

        public string CreatorCommentUserName { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
