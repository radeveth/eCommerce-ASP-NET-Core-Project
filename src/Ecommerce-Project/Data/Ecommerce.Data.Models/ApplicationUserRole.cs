namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;

    public class ApplicationUserRole : MappingModel
    {
        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationRole))]
        public string RoleId { get; set; }

        public ApplicationRole Role { get; set; }
    }
}
