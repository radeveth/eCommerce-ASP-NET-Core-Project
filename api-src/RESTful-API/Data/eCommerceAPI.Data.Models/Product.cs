namespace eCommerceAPI.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using eCommerceAPI.Data.Common.Models;
    using eCommerceAPI.Data.Models.Enums;

    using static eCommerceAPI.Data.Common.DataValidation.ProductValidation;

    public class Product : BaseDeleteableModel<int>
    {
        public Product()
        {
            this.Reviews = new HashSet<Review>();
            this.ProductCategories = new HashSet<ProductCategory>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public Status Status { get; set; }

        public int Quantity { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

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

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
