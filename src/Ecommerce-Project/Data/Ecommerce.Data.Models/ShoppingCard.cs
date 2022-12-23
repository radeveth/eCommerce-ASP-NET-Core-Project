namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;

    public class ShoppingCard : MappingModel
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        // TODO: One product have many shopping cards, but one shopping card can to have many products
        public virtual ICollection<Product> Products { get; set; }

        //[ForeignKey(nameof(Product))]
        //public int ProductId { get; set; }

        //public Product Product { get; set; }
    }
}
