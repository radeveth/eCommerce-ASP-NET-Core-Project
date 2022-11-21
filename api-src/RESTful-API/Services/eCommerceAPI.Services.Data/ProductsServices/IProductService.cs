namespace eCommerceAPI.Services.Data.ProductsServices
{
    using eCommerceAPI.InputModels.Products;
    using eCommerceAPI.ViewModels.Products;

    public interface IProductService
    {
        public Task<ProductViewModel> GetByIdAsync(int id);

        public IEnumerable<ProductViewModel> GetAll();

        public Task CreateAsync(ProductFormModel productForm);
    }
}
