namespace eCommerce.Services.Data.ProductsServices
{
    using eCommerce.ViewModels.Products;
    using eCommerce.InputModels.Products;

    public interface IProductService
    {
        public Task<ProductViewModel> GetByIdAsync(int id);

        public IEnumerable<ProductViewModel> GetAll();

        public Task CreateAsync(ProductFormModel productForm);
    }
}
