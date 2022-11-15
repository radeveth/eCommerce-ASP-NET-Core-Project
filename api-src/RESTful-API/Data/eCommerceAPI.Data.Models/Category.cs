namespace eCommerceAPI.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using eCommerceAPI.Data.Common.Models;
    using static eCommerceAPI.Data.Common.DataValidation.ProductValidation;

    public class Category : BaseDeleteableModel<int>
    {
        public Category()
        {
            this.ProductCategories = new HashSet<ProductCategory>();
        }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
