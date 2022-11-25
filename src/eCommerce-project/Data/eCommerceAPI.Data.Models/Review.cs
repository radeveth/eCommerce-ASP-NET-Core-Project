namespace eCommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using eCommerce.Data.Common.Models;
    using eCommerce.Data.Models.Enums;

    public class Review : BaseModel<int>
    {
        public ReviewScale ReviewScale { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
