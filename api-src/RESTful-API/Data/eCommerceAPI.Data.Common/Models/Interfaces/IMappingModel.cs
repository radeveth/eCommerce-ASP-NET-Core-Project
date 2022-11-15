namespace eCommerceAPI.Data.Common.Models.Interfaces
{
    public interface IMappingModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime DeletedOn { get; set; }

        public int IsDeleted { get; set; }
    }
}
