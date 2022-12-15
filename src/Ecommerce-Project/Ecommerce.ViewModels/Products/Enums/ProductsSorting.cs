namespace Ecommerce.ViewModels.Products.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ProductsSorting
    {
        [Display(Name = "The Lowest Price")]
        TheLowestPrice = 0,
        [Display(Name = "Highest Price")]
        HighestPrice = 1,
        [Display(Name = "Biggest Discount")]
        BiggestDiscount = 2,
        Latest = 3,
        Ascending = 4,
        Descending = 5,
        Rating = 6,
    }
}
