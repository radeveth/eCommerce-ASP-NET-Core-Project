namespace Ecommerce.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;
    using Ecommerce.Data.Models.Enums;

    public class AddProductReviewModel
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        [Required]
        public ReviewScale ReviewScale { get; set; }

        [Display(Name = "Comment (optional)")]
        public string Comment { get; set; }
    }
}
