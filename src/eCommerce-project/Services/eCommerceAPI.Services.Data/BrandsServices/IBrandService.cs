namespace eCommerce.Services.Data.BrandsServices
{
    using eCommerce.InputModels.Brands;

    public interface IBrandService
    {
        public Task CreateAsync(BrandFormModel brandForm);
    }
}
