namespace eCommerce.InputModels.Brands
{
    using System.ComponentModel.DataAnnotations;
    using eCommerce.Data.Models;
    using eCommerce.InputModels.ValidationAttributes;
    using static eCommerce.Data.Common.DataValidation.BrandValidation;

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
    }
}
