namespace Ecommerce.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Ecommerce.Data.Common.Models;

    using static Ecommerce.Data.Common.DataValidation.ImageValidation;

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
