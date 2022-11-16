namespace eCommerceAPI.Data.Common.Models
{
    using eCommerceAPI.Data.Common.Models.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;

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
