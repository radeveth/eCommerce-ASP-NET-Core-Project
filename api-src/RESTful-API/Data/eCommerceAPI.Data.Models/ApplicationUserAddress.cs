namespace eCommerceAPI.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using eCommerceAPI.Data.Common.Models;

    using static eCommerceAPI.Data.Common.DataValidation.ApplicationUserAddressValidation;

    public class ApplicationUserAddress : BaseDeleteableModel<int>
    {
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
    }
}
