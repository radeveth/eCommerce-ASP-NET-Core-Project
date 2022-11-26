namespace Ecommerce.Data.Common.Models
{
    using System;
    using Ecommerce.Data.Common.Models.Interfaces;

    public class MappingModel : IMappingModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime DeletedOn { get; set; }

        public int IsDeleted { get; set; }
    }
}
