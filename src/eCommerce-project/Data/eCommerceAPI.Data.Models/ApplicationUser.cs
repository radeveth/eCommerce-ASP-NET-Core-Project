namespace eCommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using eCommerce.Data.Common.Models;
    using eCommerce.Data.Models.Enums;
    using static eCommerce.Data.Common.DataValidation.ApplicationUserValidation;

    public class ApplicationUser : BaseDeleteableModel<string>
    {
        public ApplicationUser()
        {
            this.Reviews = new HashSet<Review>();
            this.Products = new HashSet<Product>();
            this.Categories = new HashSet<Category>();
            this.Orders = new HashSet<Order>();
            this.ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public Gender Gender { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
