namespace Ecommerce.Data.Common.Models.Interfaces
{
    public interface IInfoBaseModelModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
