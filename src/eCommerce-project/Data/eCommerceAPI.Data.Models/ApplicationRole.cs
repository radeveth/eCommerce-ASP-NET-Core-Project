namespace eCommerce.Data.Models
{
    using eCommerce.Data.Common.Models;

    public class ApplicationRole : BaseDeleteableModel<string>
    {
        public ApplicationRole()
        {
            this.ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public string Name { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
