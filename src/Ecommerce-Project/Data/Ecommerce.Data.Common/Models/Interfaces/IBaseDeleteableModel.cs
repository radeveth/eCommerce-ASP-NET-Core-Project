namespace Ecommerce.Data.Common.Models.Interfaces
{
    public interface IBaseDeleteableModel : IInfoBaseModelModel
    {
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
