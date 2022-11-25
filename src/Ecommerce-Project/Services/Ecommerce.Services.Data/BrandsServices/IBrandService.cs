namespace Ecommerce.Services.Data.BrandsServices
{
    using Ecommerce.InputModels.Brands;

    public interface IBrandService
    {
        public Task CreateAsync(BrandFormModel brandForm);
    }
}
