namespace Ecommerce.InputModels.Brands
{
    using System.ComponentModel.DataAnnotations;
    using Ecommerce.InputModels.ValidationAttributes;

    using static Ecommerce.Data.Common.DataValidation.BrandValidation;

    public class BrandFormModel
    {
        [Required]
        [Display(Name = "* Name: ")]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Display(Name = "Description: ")]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "Year of Fondation: ")]
        [YearMaxValue(1700, "The Year of foundation field should be between {0} and {1}.")]
        public int? YearOfFoundation { get; set; }

        public string FounderName { get; set; }
    }
}
