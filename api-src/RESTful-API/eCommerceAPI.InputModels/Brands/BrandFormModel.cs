namespace eCommerceAPI.InputModels.Brands
{
    using System.ComponentModel.DataAnnotations;
    using eCommerceAPI.Data.Models;
    using eCommerceAPI.InputModels.ValidationAttributes;
    using static eCommerceAPI.Data.Common.DataValidation.BrandValidation;

    public class BrandFormModel
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [YearMaxValue(1700, "The Year of foundation field should be between {0} and {1}.")]
        public int? YearOfFoundation { get; set; }

        public string FounderName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
