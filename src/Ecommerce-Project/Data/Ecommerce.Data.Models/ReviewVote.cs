namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;
    using Ecommerce.Data.Models.Enums;

    public class ReviewVote : BaseDeleteableModel<int>
    {
        public Vote Vote { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Review))]
        public int ReviewId { get; set; }

        public Review Review { get; set; }
    }
}
