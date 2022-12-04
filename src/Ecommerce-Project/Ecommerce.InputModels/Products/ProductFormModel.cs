namespace Ecommerce.InputModels.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Models.Enums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using static Ecommerce.Data.Common.DataValidation.ProductValidation;

    public class ProductFormModel
    {
        [Display(Name = "Name: ")]
        [Required(ErrorMessage = "Name field is required.")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Name field should be between {2} and {1}.")]
        public string Name { get; set; }

        [Display(Name = "Price: ")]
        [Required(ErrorMessage = "Price field is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status field is required.")]
        public Status Status { get; set; }

        [Display(Name = "Quantity: ")]
        [Required(ErrorMessage = "Quantity field is required.")]
        public int Quantity { get; set; }

        [Display(Name = "Description: ")]
        [Required(ErrorMessage = "Description field is required.")]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Description field should be between {2} and {1}.")]
        public string Description { get; set; }

        [FromForm]
        [NotMapped]
        public IFormFileCollection Images { get; set; }

        [Display(Name = "Category: ")]
        [Required(ErrorMessage = "Category field is required.")]
        public int CategoryId { get; set; }

        public IEnumerable<ProductCategoryFormModel> Categories { get; set; }

        [Display(Name = "Brand: ")]
        [Required(ErrorMessage = "Brand field is required.")]
        public int BrandId { get; set; }

        public IEnumerable<ProductBrandFormModel> Brands { get; set; }

        public string UserId { get; set; }
    }
}
