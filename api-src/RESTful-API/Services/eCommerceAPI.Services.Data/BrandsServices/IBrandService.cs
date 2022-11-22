namespace eCommerceAPI.Services.Data.BrandsServices
{
    using eCommerceAPI.InputModels.Brands;

    public interface IBrandService
    {
        public Task CreateAsync(BrandFormModel brandForm);
    }
}
