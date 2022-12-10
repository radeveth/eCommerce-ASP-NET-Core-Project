namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using Ecommerce.Data.Common.Models;
    using Ecommerce.Data.Models.Enums;

    public class Review : BaseModel<int>
    {
        public Review()
        {
            this.Votes = new HashSet<ReviewVote>();
        }

        public ReviewScale ReviewScale { get; set; }

        [AllowNull]
        public string? Comment { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<ReviewVote> Votes { get; set; }
    }
}
