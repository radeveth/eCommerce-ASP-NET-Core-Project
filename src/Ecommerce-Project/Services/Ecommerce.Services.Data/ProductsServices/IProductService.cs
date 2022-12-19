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
        public T GetByIdAsync<T>(int id);

        public IEnumerable<ProductViewModel> GetAll(string userId = null);

        public Task<IEnumerable<ProductViewModel>> GetAllByCategory(string category, string userId = null);

        public Task<ProductsServiceModel> GetProductsServiceModel(ProductsSorting productsSorting, string searchingName, string category, int currentPage = 1, string userId = null);

        public Task<ProductDetailsModel> Details(int id, string userId = null);

        public ProductFormModel GetProductFormModel();

        public ProductFormModel GetProductFormModelForUpdating(int id);

        // Update
        public Task UpdateAsync(int id, ProductFormModel productForm);

        public Task RestoreAsync(int id);

        public Task SetDiscountToProduct(int id, decimal discount);

        public Task UpdateDiscountStatusOfProduct(int id, bool discountStatus);

        public Task UpdateQuantityOfProduct(int id, int quantity);

        // Delete
        public Task DeleteAsync(int id);
    }
}
