namespace Ecommerce.InputModels.ApplicationUsers
{
    using System.ComponentModel.DataAnnotations;
    using Ecommerce.Data.Models.Enums;
    using static Ecommerce.Data.Common.DataValidation.ApplicationUserValidation;

    public class ApplicationUserFromModel
    {
        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        //[StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "Password should be between {2} an {1}")]
        public string PasswordHash { get; set; }

        public Gender Gender { get; set; }
    }
}
