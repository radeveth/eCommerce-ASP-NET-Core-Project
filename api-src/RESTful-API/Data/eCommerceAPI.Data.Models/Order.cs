namespace eCommerceAPI.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using eCommerceAPI.Data.Common.Models;
    using eCommerceAPI.Data.Models.Enums;

    using static eCommerceAPI.Data.Common.DataValidation.OrderValidation;

    public class Order : BaseDeleteableModel<string>
    {
        public Order()
        {
            this.Products = new HashSet<Product>();
        }

        [ForeignKey(nameof(ApplicationUser))]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        [MaxLength(CountryMaxLength)]
        public string Country { get; set; }

        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        public string PostalCode { get; set; }

        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public decimal Price { get; set; }

        public decimal ShippingPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public bool IsDelivered { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
