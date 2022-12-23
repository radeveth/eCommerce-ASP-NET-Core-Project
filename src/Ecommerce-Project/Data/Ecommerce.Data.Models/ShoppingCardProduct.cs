namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;

    public class ShoppingCardProduct : BaseMappingModel
    {
        [ForeignKey(nameof(ShoppingCard))]
        public int ShoppingCardId { get; set; }

        public ShoppingCard ShoppingCard { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
