namespace Ecommerce.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;
    using Ecommerce.ViewModels.Products.Enums;

    public class SearchingProductsCriteria
    {
        [Display(Name = "Sort By")]
        public ProductsSorting ProductsSorting { get; set; }

        public string SearchCategory { get; set; }

        public string SearchNameCriteria { get; set; }

        public decimal CheapestProduct { get; set; }
    }
}
