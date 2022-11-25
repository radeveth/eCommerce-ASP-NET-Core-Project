namespace eCommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using eCommerce.Data.Common.Models;

    using static eCommerce.Data.Common.DataValidation.ApplicationUserAddressValidation;

    public class OrderAddress : BaseDeleteableModel<int>
    {
        public OrderAddress()
        {
            this.Orders = new HashSet<Order>();
        }

        [MaxLength(CountryMaxLength)]
        public string Country { get; set; }

        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        public string PostalCode { get; set; }

        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
