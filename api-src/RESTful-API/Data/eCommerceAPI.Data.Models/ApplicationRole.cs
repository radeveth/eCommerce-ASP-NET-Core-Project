namespace eCommerceAPI.Data.Models
{
    using eCommerceAPI.Data.Common.Models;

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
