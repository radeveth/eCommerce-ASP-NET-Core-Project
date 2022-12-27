namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;

    public class ShoppingCart : BaseDeleteableModel<int>
    {
        public ShoppingCart()
        {
            this.ShoppingCardProducts = new HashSet<ShoppingCartProduct>();
        }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<ShoppingCartProduct> ShoppingCardProducts { get; set; }
    }
}
