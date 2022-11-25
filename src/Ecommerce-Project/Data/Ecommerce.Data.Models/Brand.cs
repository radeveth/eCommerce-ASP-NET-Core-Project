namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Ecommerce.Data.Common.Models;

    using static Ecommerce.Data.Common.DataValidation.BrandValidation;

    public class Brand : BaseDeleteableModel<int>
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int? YearOfFoundation { get; set; }

        public string FounderName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
