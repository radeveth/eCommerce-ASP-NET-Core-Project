namespace Ecommerce.Services.Data.ProductsServices
{
    using Ecommerce.InputModels.Products;
    using Ecommerce.ViewModels.Products;
    using Ecommerce.ViewModels.Products.Enums;

    public interface IProductService
    {
        public Task<T> GetByIdAsync<T>(int id);

        public IEnumerable<ProductViewModel> GetAll();

        public Task CreateAsync(ProductFormModel productForm);

        public Task<IEnumerable<ProductViewModel>> GetByAllProductsForCategory(string category);

        public Task<ProductsServiceModel> GetProductsServiceModel(ProductsSorting productsSorting, string category, int currentPage = 1);

        public Task<ProductDetailsModel> Details(int id);
    }
}
