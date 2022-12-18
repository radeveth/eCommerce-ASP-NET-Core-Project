namespace Ecommerce.Web.Areas.AdminPanel.Services.ProductsServices
{
    using Ecommerce.InputModels.Products;

    public interface IProductService
    {
        // Update
        public Task Update(int id, ProductFormModel productForm);

        public Task SetDiscountToProduct(int id, decimal discount);

        public Task UpdateDiscountStatusOfProduct(int id, bool discountStatus);

        public Task UpdateQuantityOfProduct(int id, int quantity);

        // Delete
        public Task DeleteAsync(int id);
    }
}
