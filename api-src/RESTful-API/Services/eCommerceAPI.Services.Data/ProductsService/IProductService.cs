namespace eCommerceAPI.Services.Data.ProductsService
{
    using eCommerceAPI.InputModels.Products;
    using eCommerceAPI.ViewModels.Products;

    public interface IProductService
    {
        public Task CreateAsync(ProductFormModel productForm);

        public Task<T> GetViewModelById<T>(int id);

        public Task<IEnumerable<ProductViewModel>> GetAll();
    }
}
