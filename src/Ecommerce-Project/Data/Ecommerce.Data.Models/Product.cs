namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using Ecommerce.Data.Common.Models;
    using Ecommerce.Data.Models.Enums;

    using static Ecommerce.Data.Common.DataValidation.ProductValidation;

    public class Product : BaseDeleteableModel<int>
    {
        public Product()
        {
            this.Reviews = new HashSet<Review>();
            this.Images = new HashSet<Image>();
            this.ProductsWishlist = new HashSet<ProductWishlist>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsHaveDiscount { get; set; }

        public decimal DiscountPercentage { get; set; }

        [Required]
        public Status Status { get; set; }

        public int Quantity { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [AllowNull]
        [ForeignKey(nameof(Order))]
        public string? OrderId { get; set; }

        public Order Order { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<ProductWishlist> ProductsWishlist { get; set; }
    }
}
