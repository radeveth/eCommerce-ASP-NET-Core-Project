namespace Ecommerce.Data.Common.Models.Interfaces
{
    public interface IBaseModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
