namespace Ecommerce.Services.Data.AdminServices
{
    using Ecommerce.ViewModels.Admin;

    public interface IAdminService
    {
        // Read
        public Task<ProductsStatisticsServiceModel> GetApplicationProductsStatistics();

        public Task<UsersStatistsicsServiceModel> GetApplicationUsersStatistics();

        // Update
        public Task DeleteUser(string id);

        public Task RestoreUser(string id);
    }
}
