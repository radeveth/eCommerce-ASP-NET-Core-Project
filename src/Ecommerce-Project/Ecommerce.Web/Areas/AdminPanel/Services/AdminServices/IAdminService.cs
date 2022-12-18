namespace Ecommerce.Web.Areas.AdminPanel.Services.AdminServices
{
    using Ecommerce.Web.Areas.AdminPanel.Models.Admin;

    public interface IAdminService
    {
        public Task<ProductsStatisticsServiceModel> GetApplicationProductsStatistics();
    }
}
