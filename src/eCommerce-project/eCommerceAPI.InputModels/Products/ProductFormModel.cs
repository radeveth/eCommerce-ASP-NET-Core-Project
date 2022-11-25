namespace eCommerce.InputModels.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using eCommerce.Data.Models;
    using eCommerce.Data.Models.Enums;

    using static eCommerce.Data.Common.DataValidation.ProductValidation;

    public class ProductFormModel
    {
        [Required(ErrorMessage = "Name field is required.")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Name field should be between {2} and {1}.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price field is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status field is required.")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Quantity field is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Description field is required.")]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Description field should be between {2} and {1}.")]
        public string Description { get; set; }

        public IEnumerable<ProductImageInputModel> Images { get; set; }

        public IEnumerable<int> Categories { get; set; }

        [Required(ErrorMessage = "Brand field is required.")]
        public int BrandId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
