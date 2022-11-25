namespace eCommerce.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PaymentMethod
    {
        Cash = 0,
        [Display(Name = "Credit Cards")]
        CreditCard = 1,
        [Display(Name = "Debit Card")]
        DebitCard = 2,
    }
}
