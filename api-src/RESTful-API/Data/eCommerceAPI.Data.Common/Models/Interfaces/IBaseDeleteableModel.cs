namespace eCommerceAPI.Data.Common.Models.Interfaces
{
    public interface IBaseDeleteableModel<TKey> : IBaseModel<TKey>
    {
        public DateTime DeletedOn { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
