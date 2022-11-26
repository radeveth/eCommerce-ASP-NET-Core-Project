namespace Ecommerce.Data.Models
{
    using Ecommerce.Data.Common.Models.Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationRole : IdentityRole, IBaseDeleteableModel
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
