namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;

    public class ShoppingCartProduct : BaseMappingModel
    {
        [ForeignKey(nameof(ShoppingCard))]
        public int ShoppingCardId { get; set; }

        public ShoppingCart ShoppingCard { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
