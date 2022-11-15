namespace eCommerceAPI.Data.Common.Models
{
    using eCommerceAPI.Data.Common.Models.Interfaces;
    using System;

    public class MappingModel : IMappingModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
        
        public DateTime DeletedOn { get; set; }
        
        public int IsDeleted { get; set; }
    }
}
