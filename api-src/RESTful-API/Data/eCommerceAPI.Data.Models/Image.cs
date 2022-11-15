namespace eCommerceAPI.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using eCommerceAPI.Data.Common.Models;

    using static eCommerceAPI.Data.Common.DataValidation.ImageValidation;

    public class Image : BaseDeleteableModel<int>
    {
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public byte[] Src { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
