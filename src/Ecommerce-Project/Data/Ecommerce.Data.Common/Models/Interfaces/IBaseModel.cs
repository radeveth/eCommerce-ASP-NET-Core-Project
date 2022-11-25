namespace Ecommerce.Data.Common.Models.Interfaces
{
    public interface IBaseModel<TKey>
    {
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
