namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;
    using Ecommerce.Data.Models.Enums;

    using static Ecommerce.Data.Common.DataValidation.OrderValidation;

    public class Order : BaseDeleteableModel<string>
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey(nameof(OrderAddress))]
        public int AddressId { get; set; }

        public OrderAddress Address { get; set; }

        public decimal Price { get; set; }

        public decimal ShippingPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public bool IsDelivered { get; set; }
    }
}
