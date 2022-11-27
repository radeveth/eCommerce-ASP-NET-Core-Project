namespace Ecommerce.Services.Data.HomeServices
{
    using Ecommerce.ViewModels.Home;

    public interface IHomeService
    {
        public Task<HomeServiceModel> GetHomeServiceModel(int countOfProductsPerCategory);
    }
}
