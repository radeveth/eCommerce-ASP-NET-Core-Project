namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;

    public class ShoppingCard : BaseDeleteableModel<int>
    {
        public ShoppingCard()
        {
            this.ShoppingCardProducts = new HashSet<ShoppingCardProduct>();
        }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<ShoppingCardProduct> ShoppingCardProducts { get; set; }
    }
}
