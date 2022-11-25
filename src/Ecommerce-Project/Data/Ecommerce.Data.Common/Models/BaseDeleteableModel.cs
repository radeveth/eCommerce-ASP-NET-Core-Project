namespace Ecommerce.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Ecommerce.Data.Common.Models.Interfaces;

    public class BaseDeleteableModel<TKey> : IBaseDeleteableModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime DeletedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
