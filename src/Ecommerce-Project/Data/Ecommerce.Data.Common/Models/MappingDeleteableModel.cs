namespace Ecommerce.Data.Common.Models
{
    using System;
    using Ecommerce.Data.Common.Models.Interfaces;

    public class MappingDeleteableModel : IMappingDeleteableModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
