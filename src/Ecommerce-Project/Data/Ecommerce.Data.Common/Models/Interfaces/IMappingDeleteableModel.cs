namespace Ecommerce.Data.Common.Models.Interfaces
{
    public interface IMappingDeleteableModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
