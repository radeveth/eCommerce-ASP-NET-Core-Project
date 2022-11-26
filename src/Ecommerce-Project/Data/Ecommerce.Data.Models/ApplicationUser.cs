namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Ecommerce.Data.Common.Models;
    using Ecommerce.Data.Common.Models.Interfaces;
    using Ecommerce.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;

    using static Ecommerce.Data.Common.DataValidation.ApplicationUserValidation;

    public class ApplicationUser : IdentityUser, IBaseDeleteableModel
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Reviews = new HashSet<Review>();
            this.Products = new HashSet<Product>();
            this.Categories = new HashSet<Category>();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        public Gender Gender { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
