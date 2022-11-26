namespace Ecommerce.Data.Common.Models
{
    using System.ComponentModel.DataAnnotations;
    using Ecommerce.Data.Common.Models.Interfaces;

    public class BaseModel<TKey> : IBaseModel<TKey>, IInfoBaseModelModel
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
