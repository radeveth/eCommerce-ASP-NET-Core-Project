namespace Ecommerce.Services.Data.ProductsServices
{
    using Ecommerce.InputModels.Products;
    using Ecommerce.ViewModels.Products;

    using Ecommerce.ViewModels.Products.Enums;

    public interface IProductService
    {
        // Create
        public Task CreateAsync(ProductFormModel productForm);

        public Task AddReviewForProduct(AddProductReviewModel productReviewModel);

        // Read
        public Task<T> GetByIdAsync<T>(int id);

        public IEnumerable<ProductViewModel> GetAll(string userId = null);

        public Task<IEnumerable<ProductViewModel>> GetAllByCategory(string category, string userId = null);

        public Task<ProductsServiceModel> GetProductsServiceModel(ProductsSorting productsSorting, string searchingName, string category, int currentPage = 1, string userId = null);

        public Task<ProductDetailsModel> Details(int id, string userId = null);

        public ProductFormModel GetProductFormModel();

        // Update

        // Delete
    }
}
