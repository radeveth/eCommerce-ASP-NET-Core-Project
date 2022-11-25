namespace Ecommerce.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ReviewScale
    {
        [Display(Name = "Very Unsatisfied")]
        VeryUnsatisfied = 1,
        Unsatisfied = 2,
        Neutral = 3,
        Satisfied = 4,
        [Display(Name = "Very Satisfied")]
        VerySatisfied = 5,
    }
}
