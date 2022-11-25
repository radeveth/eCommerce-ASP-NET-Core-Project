namespace Ecommerce.Services.Data.CategoriesServices
{
    using Ecommerce.InputModels.Categories;
    using Ecommerce.ViewModels.Categories;

    public interface ICategoryService
    {
        public Task<CategoryViewModel> GetByIdAync(int id);

        public IEnumerable<CategoryViewModel> GetAll();

        public Task CreateAsync(CategoryFormModel categoryFoem);
    }
}
