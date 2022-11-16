namespace eCommerceAPI.Services.Data.ProductsService
{
    using eCommerceAPI.InputModels.Products;

    public interface IProductService
    {
        public Task CreateAsync(ProductFormModel productForm);
    }
}
