namespace eCommerce.Data.Common.Models
{
    using eCommerce.Data.Common.Models.Interfaces;
    using System.ComponentModel.DataAnnotations;

    public class BaseModel<TKey> : IBaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
