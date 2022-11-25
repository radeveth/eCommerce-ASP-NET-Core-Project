namespace eCommerce.InputModels.Categories
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using static eCommerce.Data.Common.DataValidation.CategoryValidation;

    public class CategoryFoemModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Name field should be between {2} an {1}")]
        public string Name { get; set; }

        [StringLength(DescriptionMaxLength, ErrorMessage = "Dcription name should be lower than {1}")]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
