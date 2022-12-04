namespace Ecommerce.InputModels.Categories
{
    using System.ComponentModel.DataAnnotations;
    using static Ecommerce.Data.Common.DataValidation.CategoryValidation;

    public class CategoryFormModel
    {
        [Required]
        [Display(Name = "* Name: ")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Name field should be between {2} an {1}")]
        public string Name { get; set; }

        [Display(Name = "Description: ")]
        [StringLength(DescriptionMaxLength, ErrorMessage = "Dcription name should be lower than {1}")]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
