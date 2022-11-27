namespace Ecommerce.Services.Data.ProductsServices
{
    using Ecommerce.InputModels.Products;
    using Ecommerce.ViewModels.Products;

    public interface IProductService
    {
        public Task<ProductViewModel> GetByIdAsync(int id);

        public IEnumerable<ProductViewModel> GetAll();

        public Task CreateAsync(ProductFormModel productForm);

        public Task<IEnumerable<ProductViewModel>> GetByAllProductsForCategory(string category);
    }
}
