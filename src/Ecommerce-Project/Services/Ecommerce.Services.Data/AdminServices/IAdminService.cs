namespace Ecommerce.Services.Data.AdminServices
{
    using Ecommerce.ViewModels.Admin;

    public interface IAdminService
    {
        public Task<ProductsStatisticsServiceModel> GetApplicationProductsStatistics();
    }
}
