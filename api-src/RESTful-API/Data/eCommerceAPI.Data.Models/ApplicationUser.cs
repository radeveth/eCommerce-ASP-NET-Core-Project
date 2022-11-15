namespace eCommerceAPI.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using eCommerceAPI.Data.Common.Models;

    using static eCommerceAPI.Data.Common.DataValidation.ApplicationUserValidation;

    public class ApplicationUser : BaseDeleteableModel<string>
    {
        public ApplicationUser()
        {
            this.Reviews = new HashSet<Review>();
            this.Products = new HashSet<Product>();
            this.Orders = new HashSet<Order>();
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
        public string Password { get; set; }

        public string Phone { get; set; }

        public int MyProperty { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
